using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleConsoleApp.Library
{
    public class BattleArea
    {
        private readonly BattleField[,] _actualBattleArea;
        private readonly BattleField[,] _nextBattleArea;
        private readonly int _width;
        private readonly int _length;

        private BattleArea(int length, int width)
        {
            _length = length;
            _width = width;
            _actualBattleArea = new BattleField[_length, _width];
            _nextBattleArea = new BattleField[_length, _width];
            MakeFormation();
        }

        private void MakeFormation()
        {
            SetGreekArmy();
            SetRomanArmy();
        }

        private void SetGreekArmy()
        {
        // Greek army xd.
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    _actualBattleArea[i, j] = BattleField.Greek;
                }
            }
        }

        private void SetRomanArmy()
        {
            // Roman army xd.
            for (var i = 0; i < _width; i++)
            {
                for (var j = 9; j < 11; j++)
                {
                    _actualBattleArea[i, j] = BattleField.Roman;
                }
            }
        }

        private void LocalFight(int row, int col)
        {
            BattleField warrior = _actualBattleArea[row, col];
            if (warrior == BattleField.Walkable || warrior == BattleField.NotWalkable)
                return;
            BattleField oppositeWarrior = (warrior == BattleField.Greek) 
                ? BattleField.Roman 
                : BattleField.Greek;
            CountWarriors(out int numberOfWarrior, out int numberOfOppositeWarrior, row, col, warrior);
            Fight(numberOfWarrior, numberOfOppositeWarrior, row, col, warrior, oppositeWarrior);
        }

        private void CountWarriors(out int numberOfWarrior, out int numberOfOppositeWarrior, 
            int row, int col, BattleField warrior)
        {
            numberOfWarrior = 0;
            numberOfOppositeWarrior = 0;
            for (var i = row - 1; i <= col + 1; i++)
            {
                for (var j = col - 1; j <= col + 1; j++)
                {
                    if (_actualBattleArea[i, j] == warrior)
                    {
                        numberOfWarrior++;
                    }
                    else
                    {
                        numberOfOppositeWarrior++;
                    }
                }
            }
                
        }

        private void Fight(int numberOfWarrior, int numberOfOppositeWarrior, int row, int col,
            BattleField warrior, BattleField oppositeWarrior)
        {
            if (numberOfWarrior < numberOfOppositeWarrior)
            {
                _nextBattleArea[row, col] = BattleField.Walkable; 
            }
            else if (numberOfWarrior == numberOfOppositeWarrior)
            {
                _nextBattleArea[row, col] = (warrior > oppositeWarrior)
                    ? warrior
                    : BattleField.Walkable;
            }
        }

        private void Walk(int row, int col, BattleField warrior)
        {
            var ratio = _length / _width;
            if ((ratio * row > col) && (_width - ratio * row < col))
            {
                // prawo.
            }
            else if ((ratio * row < col) && (_width - ratio * row < col))
            {
                // dol.
            }
            else if ((ratio * row < col) && (_width - ratio * row > col))
            {
                // lewo.
            }
            else if ((ratio * row > col) && (_width - ratio * row > col))
            {
                // gora.
            }
        }

    }
}
