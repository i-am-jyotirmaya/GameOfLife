using System;
using System.Collections.Generic;
using System.Linq;

namespace GameUtilities
{
    public class Game
    {
        private Cell[] _initialState;

        private Cell[] _currentState;

        private List<Cell> _impactedCells; //List of cells that will be impacted by the current tick

        public void SetInitialState(Cell[] cells)
        {
            _initialState = cells;
            _currentState = _initialState.ToArray();
        }

        //Add the cells surrounding the alive cells to the impacted list
        private void _includeOuterCells()
        {
            foreach(Cell cell in _currentState)
            {
                Cell[] surroundingCells = new Cell[] {
                    new Cell(cell.X, cell.Y + 1, false),
                    new Cell(cell.X + 1, cell.Y + 1, false),
                    new Cell(cell.X + 1, cell.Y, false),
                    new Cell(cell.X + 1, cell.Y - 1, false),
                    new Cell(cell.X, cell.Y - 1, false),
                    new Cell(cell.X - 1, cell.Y - 1, false),
                    new Cell(cell.X - 1, cell.Y, false),
                    new Cell(cell.X - 1, cell.Y + 1, false),
                };

                foreach (Cell surrCell in surroundingCells)
                {
                    if (!_impactedCells.Any(c => c.X == surrCell.X && c.Y == surrCell.Y))
                    {
                        _impactedCells.Add(surrCell);
                    }
                }
            }
        }

        //Start ticks
        public void ProceedTicks(int noOfTicks)
        {
            for (int ticksCounter = 0; ticksCounter < noOfTicks; ticksCounter++)
            {
                _impactedCells = new List<Cell>(_currentState);
                _includeOuterCells();
                _tick();
            }
        }

        //To fetch the current state of cells in string format
        public string getCurrentState()
        {
            List<string> result = new List<string>();

            for (int stateCounter = 0; stateCounter < _currentState.Length; stateCounter++)
            {
                if (!_currentState[stateCounter].IsAlive) continue;
                string resultString = _currentState[stateCounter].X + ", " + _currentState[stateCounter].Y;
                result.Add(resultString);
            }

            return string.Join("\n", result.ToArray());
        }

        private void _tick()
        {
            List<Cell> newState = new List<Cell>();

            foreach (Cell cell in _impactedCells)
            {
                newState.Add(_doCellOperation(cell));
            }

            _currentState = newState.ToArray();            
        }

        //Changes the cell state based on rules on each tick
        private Cell _doCellOperation(Cell cell)
        {
            Cell copiedCell = new Cell(cell);
            int surroundingAliveCells = _getSurroundingAliveCellsCount(cell);
            if (surroundingAliveCells < 2 || surroundingAliveCells > 3)
            {
                copiedCell.kill();
            }
            else if (surroundingAliveCells == 3 && !cell.IsAlive)
            {
                copiedCell.resurrect();
            }

            return copiedCell;
        }

        //Returns the number of alive cells surrounding the cell passed as argument
        private int _getSurroundingAliveCellsCount(Cell cell)
        {
            int aliveCellCount = 0;

            if (_isCellAlive(cell.X, cell.Y + 1))
            {
                aliveCellCount++;
            }
            if (_isCellAlive(cell.X + 1, cell.Y + 1))
            {
                aliveCellCount++;
            }
            if (_isCellAlive(cell.X + 1, cell.Y))
            {
                aliveCellCount++;
            }
            if (_isCellAlive(cell.X + 1, cell.Y - 1))
            {
                aliveCellCount++;
            }
            if (_isCellAlive(cell.X, cell.Y - 1))
            {
                aliveCellCount++;
            }
            if (_isCellAlive(cell.X - 1, cell.Y - 1))
            {
                aliveCellCount++;
            }
            if (_isCellAlive(cell.X - 1, cell.Y))
            {
                aliveCellCount++;
            }
            if (_isCellAlive(cell.X - 1, cell.Y + 1))
            {
                aliveCellCount++;
            }

            return aliveCellCount;
        }

        private bool _isCellAlive(int x, int y)
        {
            for (int cellCounter = 0; cellCounter < _currentState.Length; cellCounter++)
            {
                if (_currentState[cellCounter].X == x && _currentState[cellCounter].Y == y)
                {
                    return _currentState[cellCounter].IsAlive;
                }
            }

            return false;
        }
    }

    // Cell class
    public class Cell
    {
        //Cell coordinates
        public int X { get; }
        public int Y { get; }
        public bool IsAlive { get; private set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            IsAlive = true;
        }

        public Cell(int x, int y, bool isAlive)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }

        //Copy constructor
        public Cell(Cell cell)
        {
            X = cell.X;
            Y = cell.Y;
            IsAlive = cell.IsAlive;
        }

        //To Kill a cell
        public void kill()
        {
            IsAlive = false;
        }

        //To revive a cell
        public void resurrect()
        {
            IsAlive = true;
        }
    }
}