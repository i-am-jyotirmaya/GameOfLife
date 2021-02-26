using GameUtilities;
using NUnit.Framework;

namespace GameOfLifeTest
{
    class DoubleOsc
    {
        private Cell[] cells;
        private Game game;

        [SetUp]
        public void Setup()
        {
            cells = new Cell[] {
                new Cell(1, 1),
                new Cell(1, 2),
                new Cell(1, 3),
                new Cell(2, 2),
                new Cell(2, 3),
                new Cell(2, 4)
            };

            game = new Game();
            game.SetInitialState(cells);

            game.ProceedTicks(1);
        }

        [Test]
        public void TestResult()
        {
            string res = game.getCurrentState();
            Assert.IsTrue(res.Contains("0, 2"));
            Assert.IsTrue(res.Contains("1, 1"));
            Assert.IsTrue(res.Contains("2, 1"));
            Assert.IsTrue(res.Contains("1, 4"));
            Assert.IsTrue(res.Contains("2, 4"));
            Assert.IsTrue(res.Contains("3, 3"));
        }
    }
}
