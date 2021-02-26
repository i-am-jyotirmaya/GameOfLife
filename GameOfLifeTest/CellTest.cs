using GameUtilities;
using NUnit.Framework;

namespace GameOfLifeTest
{
    public class CellTests
    {
        private Cell cell;
        private Cell cell1;

        [SetUp]
        public void Setup()
        {
            cell = new Cell(1, 1, false);
            cell1 = new Cell(0, 0);
        }

        [Test]
        public void Test1()
        {
            Assert.IsFalse(cell.IsAlive);
        }

        [Test]
        public void Test2()
        {
            Assert.IsTrue(cell1.IsAlive);
        }
    }
}
