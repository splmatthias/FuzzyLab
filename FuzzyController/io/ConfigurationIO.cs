using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using fuzzyController.expressions;
using fuzzyController.expressions.visitors;
using fuzzyController.variables;
using Newtonsoft.Json;

namespace fuzzyController.io
{
    public class ConfigurationIO
    {
        public void WriteToFile(FuzzyConfiguration configuration, string filePath)
        {
            var exprConverter = new JsonConfigurationConverter();
            var jsonConfig = new JSonFuzzyConfiguration
            {
                NumericVariables = configuration.NumericVariables.Select(n => new JsonNumericVariable
                {
                    Identifier = n.Identifier,
                    MinValue = Math.Abs(n.MinValue - double.MinValue) < 0.000001 ? (double?) null : n.MinValue,
                    MaxValue = Math.Abs(n.MaxValue - double.MaxValue) < 0.000001 ? (double?) null : n.MaxValue
                }),
                FuzzyVariables = configuration.FuzzyVariables.Select(v => new JsonFuzzyVariable
                {
                    Identifier = v.Identifier,
                    NumericVariable = v.NumericVariable != null ? v.NumericVariable.Identifier : null,
                    FuzzyTerms = v.FuzzyTerms.Select(t => new JsonFuzzyTerm
                    {
                        Term = t.Term,
                        MembershipFunction = t.MembershipFunction
                    })
                }),
                Fuzzification = configuration.Fuzzification.Select(v => v.Identifier),
                Iterations = configuration.Iterations.Select(i => new JsonIteration
                {
                    Implications = i.Implications.Select(r => new JsonFuzzyImplication
                    {
                        Conclusion = r.Conclusion.Accept(exprConverter),
                        Premise = r.Premise.Accept(exprConverter)
                    })
                }),
                Defuzzification = configuration.Defuzzification.Select(v => v.Identifier)
            };
            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(JsonConvert.SerializeObject(jsonConfig, Formatting.Indented));
            }
        }

        public FuzzyConfiguration ReadFromFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();

                var jsonConfig = JsonConvert.DeserializeObject<JSonFuzzyConfiguration>(content);

                return convertToFuzzyConfiguration(jsonConfig);
            }
        }

        private FuzzyConfiguration convertToFuzzyConfiguration(JSonFuzzyConfiguration jsonConfig)
        {
            var numericVariables = jsonConfig.NumericVariables.Select(n => new NumericVariable(n.Identifier, n.MinValue, n.MaxValue)).ToList();
            var fuzzyVariables = jsonConfig.FuzzyVariables.Select(f =>
            {
                var numVar = numericVariables.FirstOrDefault(n => n.Identifier == f.NumericVariable);
                var terms = f.FuzzyTerms.Select(t => new FuzzyTerm(t.Term, t.MembershipFunction)).ToArray();
                return new FuzzyVariable(f.Identifier, numVar, terms);
            }).ToList();
            var fuzzification = jsonConfig.Fuzzification.Select(identifier => fuzzyVariables.FirstOrDefault(v => v.Identifier == identifier)).ToList();

            var iterations = jsonConfig.Iterations.Select(i => new Iteration(i.Implications.Select(impl =>
            {
                var premise = exprFromJson(impl.Premise, fuzzyVariables);
                var conclusion = exprFromJson(impl.Conclusion, fuzzyVariables) as ValueExpression;
                return new FuzzyImplication(premise, conclusion);
            }))).ToList();

            var defuzzification = jsonConfig.Defuzzification.Select(identifier => fuzzyVariables.FirstOrDefault(v => v.Identifier == identifier)).ToList();

            return new FuzzyConfiguration(numericVariables, fuzzyVariables, fuzzification, iterations, defuzzification);
        }
        
        private IFuzzyExpression exprFromJson(JsonFuzzyExpression jsonExpr, IEnumerable<FuzzyVariable> variables )
        {
            if(jsonExpr.Type == "NOT")
                return new NotExpression(exprFromJson(jsonExpr.SubExpressions.ToArray()[0], variables));
            if (jsonExpr.Type == "VALUE")
            {
                var fuzzyVariable = variables.FirstOrDefault(v => v.Identifier == jsonExpr.Variable);
                return new ValueExpression(
                    fuzzyVariable,
                    fuzzyVariable.FuzzyTerms.FirstOrDefault(v => v.Term == jsonExpr.Value));
            }
            if (jsonExpr.Type == "OR")
                return new OrExpression(
                            exprFromJson(jsonExpr.SubExpressions.ToArray()[0], variables),
                            exprFromJson(jsonExpr.SubExpressions.ToArray()[1], variables));
            if (jsonExpr.Type == "AND")
                return new AndExpression(
                            exprFromJson(jsonExpr.SubExpressions.ToArray()[0], variables),
                            exprFromJson(jsonExpr.SubExpressions.ToArray()[1], variables));

            throw new NotSupportedException("Unkown type");
        }

        private class JSonFuzzyConfiguration
        {
            public IEnumerable<JsonNumericVariable> NumericVariables { get; set; }

            public IEnumerable<JsonFuzzyVariable> FuzzyVariables { get; set; }

            public IEnumerable<string> Fuzzification { get; set; }

            public IEnumerable<JsonIteration> Iterations { get; set; }

            public IEnumerable<string> Defuzzification { get; set; }
        }

        private class JsonConfigurationConverter : IExpressionVisitor<JsonFuzzyExpression>
        {
            public JsonFuzzyExpression Visit(ValueExpression expr)
            {
                return new JsonFuzzyExpression
                {
                    Type = "VALUE",
                    Variable =  expr.Variable.Identifier,
                    Value = expr.Value.Term
                };
            }

            public JsonFuzzyExpression Visit(NotExpression expr)
            {
                return new JsonFuzzyExpression
                {
                    Type = "NOT",
                    SubExpressions = new List<JsonFuzzyExpression> {expr.Expression.Accept(this)}
                };
            }

            public JsonFuzzyExpression Visit(AndExpression expr)
            {
                return new JsonFuzzyExpression
                {
                    Type = "AND",
                    SubExpressions = new List<JsonFuzzyExpression> { expr.LeftExpression.Accept(this), expr.RightExpression.Accept(this) }
                };
            }

            public JsonFuzzyExpression Visit(OrExpression expr)
            {
                return new JsonFuzzyExpression
                {
                    Type = "OR",
                    SubExpressions = new List<JsonFuzzyExpression> { expr.LeftExpression.Accept(this), expr.RightExpression.Accept(this) }
                };
            }

            public JsonFuzzyExpression Visit(FuzzyImplication implication)
            {
                throw new NotImplementedException();
            }
        }


        public class JsonIteration
        {
            public IEnumerable<JsonFuzzyImplication> Implications { get; set; }
        }

        public class JsonFuzzyImplication
        {
            public JsonFuzzyExpression Premise { get; set; }
            public JsonFuzzyExpression Conclusion { get; set; }
        }

        public class JsonFuzzyExpression
        {
            public string Type { get; set; }
            public IEnumerable<JsonFuzzyExpression> SubExpressions { get; set; }
            public string Variable { get; set; }
            public string Value { get; set; }
        }

        private class JsonFuzzyVariable
        {
            public string Identifier { get; set; }
            public string NumericVariable { get; set; }
            public IEnumerable<JsonFuzzyTerm> FuzzyTerms { get; set; }
        }

        private class JsonFuzzyTerm
        {
            public string Term { get; set; }
            public MembershipFunction MembershipFunction { get; set; }
        }

        private class JsonNumericVariable
        {
            public string Identifier { get; set; }
            public double? MinValue { get; set; }
            public double? MaxValue { get; set; }
        }
    }
}
