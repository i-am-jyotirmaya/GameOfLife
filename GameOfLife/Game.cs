using System;
using System.Collections.Generic;
using System.Linq;

namespace GameUtilities
{
    public class Game
    {
        private Cell[] _initialState;

        private Cell[] _currentState;

        private List<Cell> _impactedCells;

        public void SetInitialState(Cell[] cells)
        {
            _initialState = cells;
            _currentState = _initialState.ToArray();

            _impactedCells = new List<Cell>(_currentState);
            _includeOuterCells();
        }

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

        public void ProceedTicks(int noOfTicks)
        {
            for (int ticksCounter = 0; ticksCounter < noOfTicks; ticksCounter++)
            {
                _tick();
            }
        }

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

    public class Cell
    {
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

        public Cell(Cell cell)
        {
            X = cell.X;
            Y = cell.Y;
            IsAlive = cell.IsAlive;
        }

        public void kill()
        {
            IsAlive = false;
        }

        public void resurrect()
        {
            IsAlive = true;
        }
    }
}