using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using RoboSim.viewModels;

namespace RoboSim.persistency
{
    public class ConfigReaderWriter
    {
        private static void indentLine(StringBuilder content, string line, int indentation)
        {
            content.AppendLine(new String(' ', 4*indentation) + line);
        }

        public void WriteToFile(IEnumerable<SituationViewModel> situations, string file)
        {
            var content = SituationToString(situations);

            var writer = new StreamWriter(file);

            writer.Write(content);

            writer.Close();
        }

        public string SituationToString(IEnumerable<SituationViewModel> situations)
        {
            var result = new StringBuilder();

            result.AppendLine("<?xml version=\"1.0\"?>");
            result.AppendLine("<situations>");

            foreach (var situation in situations)
            {
                indentLine(result, "<situation name=\"" + situation.Name + "\" value=\"" + situation.SituationValue + "\">", 1);

                indentLine(result, "<ball state=\"" + situation.Ball.States.SelectedItem + "\" x=\"" + situation.Ball.X + "\" y=\"" + situation.Ball.Y + "\"/>", 2);

                indentLine(result, "<team>", 2);
                foreach (var player in situation.OwnTeam)
                {
                    indentLine(result, "<player id=\"" + player.No + "\" state=\"" + player.States.SelectedItem + "\" x=\"" + player.X + "\" y=\"" + player.Y + "\"/>", 3);
                }
                indentLine(result, "</team>", 2);
                indentLine(result, "<opponents>", 2);
                foreach (var player in situation.Opponents)
                {
                    indentLine(result, "<player id=\"" + player.No + "\" state=\"" + player.States.SelectedItem + "\" x=\"" + player.X + "\" y=\"" + player.Y + "\"/>", 3);
                }
                indentLine(result, "</opponents>", 2);

                indentLine(result, "</situation>", 1);
            }

            result.Append("</situations>");

            return result.ToString();
        }

        public IList<SituationViewModel> StringToSituations(string content)
        {
            var result = new List<SituationViewModel>();

            var doc = new XmlDocument();
            doc.LoadXml(content);

            var rootNode = doc.ChildNodes[1];
            if (rootNode.Name != "situations")
                throw new FormatException("Unable to find situations node");

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Name != "situation")
                    throw new FormatException("Encountered unknown node type.");

                var situation = new SituationViewModel();
                situation.Name = node.Attributes["name"].Value;
                situation.SituationValue = Convert.ToDouble(node.Attributes["value"].Value);

                var ballNode = node.ChildNodes[0];
                if(ballNode.Name != "ball")
                    throw new FormatException("Unable to finde ball node");

                situation.Ball.States.SelectedItem = ballNode.Attributes["state"].Value;
                situation.Ball.X = Convert.ToDouble(ballNode.Attributes["x"].Value);
                situation.Ball.Y = Convert.ToDouble(ballNode.Attributes["y"].Value);

                var teamNode = node.ChildNodes[1];
                if(teamNode.Name != "team")
                    throw new FormatException("Encountered unexpected node type");
                loadPlayer(situation.OwnTeam, teamNode);
                
                var opponentsNode = node.ChildNodes[2];
                if (opponentsNode.Name != "opponents")
                    throw new FormatException("Encountered unexpected node type");
                loadPlayer(situation.Opponents, opponentsNode);

                result.Add(situation);
            }

            return result;
        }

        private void loadPlayer(ObservableCollection<PlayerViewModel> players, XmlNode teamNode)
        {
            foreach (XmlNode playerNode in teamNode.ChildNodes)
            {
                if (playerNode.Name != "player")
                    throw new FormatException("Encountered unexpected node type");
                var id = Convert.ToInt32(playerNode.Attributes["id"].Value);
                var player = players.FirstOrDefault(p => p.No == id);
                if (player == null)
                    throw new FormatException("Unknown id for player");
                player.States.SelectedItem = playerNode.Attributes["state"].Value;
                player.X = Convert.ToDouble(playerNode.Attributes["x"].Value);
                player.Y = Convert.ToDouble(playerNode.Attributes["y"].Value);
            }
        }

        public IList<SituationViewModel> ReadFromFile(string file)
        {
            var reader = new StreamReader(file);
            var content = reader.ReadToEnd();

            return StringToSituations(content);
        }
    }
}
