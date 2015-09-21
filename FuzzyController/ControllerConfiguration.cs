using fuzzyController.defuzzifier;
using fuzzyController.fuzzifier;
using fuzzyController.inference;
using fuzzyController.inference.valueMerger;

namespace fuzzyController
{
    public class ControllerConfiguration
    {
        public ControllerConfiguration(IFuzzifier fuzzifier, 
                                       IRuleEvaluation inference, 
                                       IFuzzyValueMerger valueMerger, 
                                       IDefuzzifier defuzzifier)
        {
            Fuzzifier = fuzzifier;
            Inference = inference;
            ValueMerger = valueMerger;
            Defuzzifier = defuzzifier;
        }

        public readonly IFuzzifier Fuzzifier;

        public readonly IRuleEvaluation Inference;

        public readonly IFuzzyValueMerger ValueMerger;

        public readonly IDefuzzifier Defuzzifier;
    }
}
