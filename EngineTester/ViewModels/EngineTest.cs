using Engine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTester.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void TestPlayerMovesHomeAndIsCompletelyHealedOnKilled()
        {
            GameSession gameSession = new GameSession();
            gameSession.CurrentPlayer.TakeDamage(999);
            Assert.AreEqual("House", gameSession.CurrentLocation.Name);
            Assert.AreEqual(gameSession.CurrentPlayer.Level * 10, gameSession.CurrentPlayer.CurrentHitPoints);
        }

        [TestMethod]
        public void TestCreateGameSession()
        {

            GameSession gameSession = new GameSession();

            // Game session data
            Assert.AreEqual(0, gameSession.CurrentLocation.XCoordinate);
            Assert.AreEqual(0, gameSession.CurrentLocation.YCoordinate);

            // Player data
            Assert.AreEqual("Maple", gameSession.CurrentPlayer.Name);
            Assert.AreEqual(10, gameSession.CurrentPlayer.CurrentHitPoints);
            Assert.AreEqual(10, gameSession.CurrentPlayer.MaximumHitPoints);
            Assert.AreEqual(0, gameSession.CurrentPlayer.ExperiencePoints);
            Assert.AreEqual(1, gameSession.CurrentPlayer.Level);
        }
    }
}
