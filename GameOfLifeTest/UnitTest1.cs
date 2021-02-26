using GameUtilities;
using NUnit.Framework;
using System.Reflection;

namespace GameOfLifeTest
{
    public class GameTests
    {
        private Cell[] cells;
        private Game game;

        [SetUp]
        public void Setup()
        {
            cells = new Cell[] {
                new Cell(1, 1),
                new Cell(1, 2),
                new Cell(2, 1)
            };

            game = new Game();
            game.SetInitialState(cells);

            game.ProceedTicks(1);
        }

        [Test]
        public void Test1()
        {
            string res = game.getCurrentState();
            Assert.IsTrue(res.Contains("2, 2"));
            Assert.IsTrue(res.Contains("1, 1"));
        }

        [Test]
        public void Test2()
        {
            string res = game.getCurrentState();
            Assert.IsTrue(res.Contains("2, 1"));
        }


    }
}