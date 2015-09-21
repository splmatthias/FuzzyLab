using NUnit.Framework;
using RoboSim.persistency;
using RoboSim.viewModels;

namespace RoboSim.Test.persistency
{
    [TestFixture]
    public class ConfigReaderWriterTest
    {
        [Test]
        public void ReadFromXmlString()
        {
            var content = @"<?xml version=""1.0""?>
<situations>
    <situation name=""situation1"" value=""0,5"">
        <ball state=""InGame"" x=""1"" y=""10""/>
        <team>
            <player id=""1"" state=""Available""    x=""1"" y=""10""/>
            <player id=""2"" state=""Penalized""  x=""2"" y=""20""/>
            <player id=""3"" state=""Disabled""   x=""3"" y=""30""/>
            <player id=""4"" state=""NotPlaying"" x=""4"" y=""40""/>
            <player id=""5"" state=""Available""    x=""5"" y=""50""/>
        </team>
        <opponents>
            <player id=""1"" state=""Available""    x=""-1"" y=""-10""/>
            <player id=""2"" state=""Penalized""  x=""-2"" y=""-20""/>
            <player id=""3"" state=""Disabled""   x=""-3"" y=""-30""/>
            <player id=""4"" state=""NotPlaying"" x=""-4"" y=""-40""/>
            <player id=""5"" state=""Available""    x=""-5"" y=""-50""/>
        </opponents>
    </situation>
    <situation name=""situation2"" value=""-0,5"">
        <ball state=""NotInGame"" x=""-10"" y=""-100""/>
        <team>
            <player id=""1"" state=""Available""    x=""1"" y=""10""/>
            <player id=""2"" state=""Penalized""  x=""2"" y=""20""/>
            <player id=""3"" state=""Disabled""   x=""3"" y=""30""/>
            <player id=""4"" state=""NotPlaying"" x=""4"" y=""40""/>
            <player id=""5"" state=""Available""    x=""5"" y=""50""/>
        </team>
        <opponents>
            <player id=""1"" state=""Available""    x=""-1"" y=""-10""/>
            <player id=""2"" state=""Penalized""  x=""-2"" y=""-20""/>
            <player id=""3"" state=""Disabled""   x=""-3"" y=""-30""/>
            <player id=""4"" state=""NotPlaying"" x=""-4"" y=""-40""/>
            <player id=""5"" state=""Available""    x=""-5"" y=""-50""/>
        </opponents>
    </situation>
</situations>";

            var sut = new ConfigReaderWriter();

            var result = sut.StringToSituations(content);


            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("situation1", result[0].Name);
            Assert.AreEqual(0.5, result[0].SituationValue, 0.00000000001);
            Assert.AreEqual("InGame", result[0].Ball.States.SelectedItem);
            Assert.AreEqual(1, result[0].Ball.X, 0.00000000001);
            Assert.AreEqual(10, result[0].Ball.Y, 0.00000000001);
            Assert.AreEqual(5, result[0].OwnTeam.Count);

            assertPlayer(1, "Available",    1, 10, result[0].OwnTeam[0]);
            assertPlayer(2, "Penalized",  2, 20, result[0].OwnTeam[1]);
            assertPlayer(3, "Disabled",   3, 30, result[0].OwnTeam[2]);
            assertPlayer(4, "NotPlaying", 4, 40, result[0].OwnTeam[3]);
            assertPlayer(5, "Available",    5, 50, result[0].OwnTeam[4]);

            assertPlayer(1, "Available",    -1, -10, result[0].Opponents[0]);
            assertPlayer(2, "Penalized",  -2, -20, result[0].Opponents[1]);
            assertPlayer(3, "Disabled",   -3, -30, result[0].Opponents[2]);
            assertPlayer(4, "NotPlaying", -4, -40, result[0].Opponents[3]);
            assertPlayer(5, "Available",    -5, -50, result[0].Opponents[4]);


            Assert.AreEqual("situation2", result[1].Name);
            Assert.AreEqual(-0.5, result[1].SituationValue, 0.00000000001);
            Assert.AreEqual("NotInGame", result[1].Ball.States.SelectedItem);
            Assert.AreEqual(-10, result[1].Ball.X, 0.00000000001);
            Assert.AreEqual(-100, result[1].Ball.Y, 0.00000000001);
            Assert.AreEqual(5, result[1].OwnTeam.Count);

            assertPlayer(1, "Available",    1, 10, result[1].OwnTeam[0]);
            assertPlayer(2, "Penalized",  2, 20, result[1].OwnTeam[1]);
            assertPlayer(3, "Disabled",   3, 30, result[1].OwnTeam[2]);
            assertPlayer(4, "NotPlaying", 4, 40, result[1].OwnTeam[3]);
            assertPlayer(5, "Available",    5, 50, result[1].OwnTeam[4]);

            assertPlayer(1, "Available",    -1, -10, result[1].Opponents[0]);
            assertPlayer(2, "Penalized",  -2, -20, result[1].Opponents[1]);
            assertPlayer(3, "Disabled",   -3, -30, result[1].Opponents[2]);
            assertPlayer(4, "NotPlaying", -4, -40, result[1].Opponents[3]);
            assertPlayer(5, "Available",    -5, -50, result[1].Opponents[4]);
        }

        private static void assertPlayer(int id, string state, double x, double y, PlayerViewModel player)
        {
            Assert.AreEqual(id, player.No);
            Assert.AreEqual(state, player.States.SelectedItem);
            Assert.AreEqual(x, player.X, 0.00000000001);
            Assert.AreEqual(y, player.Y, 0.00000000001);
        }


        [Test]
        public void WriteToXmlString()
        {
            var situation1 = new SituationViewModel();
            situation1.Name = "situation1";
            situation1.SituationValue = 0.5;
            situation1.Ball.X = 1;
            situation1.Ball.Y = 10;
            situation1.OwnTeam[0].States.SelectedItem = "Available";
            situation1.OwnTeam[0].X = 1;
            situation1.OwnTeam[0].Y = 10;
            situation1.OwnTeam[1].States.SelectedItem = "Penalized";
            situation1.OwnTeam[1].X = 2;
            situation1.OwnTeam[1].Y = 20;
            situation1.OwnTeam[2].States.SelectedItem = "Disabled";
            situation1.OwnTeam[2].X = 3;
            situation1.OwnTeam[2].Y = 30;
            situation1.OwnTeam[3].States.SelectedItem = "NotPlaying";
            situation1.OwnTeam[3].X = 4;
            situation1.OwnTeam[3].Y = 40;
            situation1.OwnTeam[4].States.SelectedItem = "Available";
            situation1.OwnTeam[4].X = 5;
            situation1.OwnTeam[4].Y = 50;

            situation1.Opponents[0].States.SelectedItem = "Available";
            situation1.Opponents[0].X = -1;
            situation1.Opponents[0].Y = -10;
            situation1.Opponents[1].States.SelectedItem = "Penalized";
            situation1.Opponents[1].X = -2;
            situation1.Opponents[1].Y = -20;
            situation1.Opponents[2].States.SelectedItem = "Disabled";
            situation1.Opponents[2].X = -3;
            situation1.Opponents[2].Y = -30;
            situation1.Opponents[3].States.SelectedItem = "NotPlaying";
            situation1.Opponents[3].X = -4;
            situation1.Opponents[3].Y = -40;
            situation1.Opponents[4].States.SelectedItem = "Available";
            situation1.Opponents[4].X = -5;
            situation1.Opponents[4].Y = -50;


            var situation2 = new SituationViewModel();
            situation2.Name = "situation2";
            situation2.SituationValue = -0.5;
            situation2.Ball.States.SelectedItem = "NotInGame";
            situation2.Ball.X = -10;
            situation2.Ball.Y = -100;
            situation2.OwnTeam[0].States.SelectedItem = "Available";
            situation2.OwnTeam[0].X = 1;
            situation2.OwnTeam[0].Y = 10;
            situation2.OwnTeam[1].States.SelectedItem = "Penalized";
            situation2.OwnTeam[1].X = 2;
            situation2.OwnTeam[1].Y = 20;
            situation2.OwnTeam[2].States.SelectedItem = "Disabled";
            situation2.OwnTeam[2].X = 3;
            situation2.OwnTeam[2].Y = 30;
            situation2.OwnTeam[3].States.SelectedItem = "NotPlaying";
            situation2.OwnTeam[3].X = 4;
            situation2.OwnTeam[3].Y = 40;
            situation2.OwnTeam[4].States.SelectedItem = "Available";
            situation2.OwnTeam[4].X = 5;
            situation2.OwnTeam[4].Y = 50;

            situation2.Opponents[0].States.SelectedItem = "Available";
            situation2.Opponents[0].X = -1;
            situation2.Opponents[0].Y = -10;
            situation2.Opponents[1].States.SelectedItem = "Penalized";
            situation2.Opponents[1].X = -2;
            situation2.Opponents[1].Y = -20;
            situation2.Opponents[2].States.SelectedItem = "Disabled";
            situation2.Opponents[2].X = -3;
            situation2.Opponents[2].Y = -30;
            situation2.Opponents[3].States.SelectedItem = "NotPlaying";
            situation2.Opponents[3].X = -4;
            situation2.Opponents[3].Y = -40;
            situation2.Opponents[4].States.SelectedItem = "Available";
            situation2.Opponents[4].X = -5;
            situation2.Opponents[4].Y = -50;


            var sut = new ConfigReaderWriter();

            var result = sut.SituationToString(new[] {situation1, situation2});


            var expected = @"<?xml version=""1.0""?>
<situations>
    <situation name=""situation1"" value=""0,5"">
        <ball state=""InGame"" x=""1"" y=""10""/>
        <team>
            <player id=""1"" state=""Available"" x=""1"" y=""10""/>
            <player id=""2"" state=""Penalized"" x=""2"" y=""20""/>
            <player id=""3"" state=""Disabled"" x=""3"" y=""30""/>
            <player id=""4"" state=""NotPlaying"" x=""4"" y=""40""/>
            <player id=""5"" state=""Available"" x=""5"" y=""50""/>
        </team>
        <opponents>
            <player id=""1"" state=""Available"" x=""-1"" y=""-10""/>
            <player id=""2"" state=""Penalized"" x=""-2"" y=""-20""/>
            <player id=""3"" state=""Disabled"" x=""-3"" y=""-30""/>
            <player id=""4"" state=""NotPlaying"" x=""-4"" y=""-40""/>
            <player id=""5"" state=""Available"" x=""-5"" y=""-50""/>
        </opponents>
    </situation>
    <situation name=""situation2"" value=""-0,5"">
        <ball state=""NotInGame"" x=""-10"" y=""-100""/>
        <team>
            <player id=""1"" state=""Available"" x=""1"" y=""10""/>
            <player id=""2"" state=""Penalized"" x=""2"" y=""20""/>
            <player id=""3"" state=""Disabled"" x=""3"" y=""30""/>
            <player id=""4"" state=""NotPlaying"" x=""4"" y=""40""/>
            <player id=""5"" state=""Available"" x=""5"" y=""50""/>
        </team>
        <opponents>
            <player id=""1"" state=""Available"" x=""-1"" y=""-10""/>
            <player id=""2"" state=""Penalized"" x=""-2"" y=""-20""/>
            <player id=""3"" state=""Disabled"" x=""-3"" y=""-30""/>
            <player id=""4"" state=""NotPlaying"" x=""-4"" y=""-40""/>
            <player id=""5"" state=""Available"" x=""-5"" y=""-50""/>
        </opponents>
    </situation>
</situations>";

            Assert.AreEqual(expected, result);
        }
    }
}
