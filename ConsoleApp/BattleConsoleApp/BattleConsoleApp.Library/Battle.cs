using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleConsoleApp.Library
{
    public class Battle
    {
        public BattleArea BattleArea { get; set; }
        public Battle()
        {
            BattleArea = new BattleArea(10, 20);
        }

        public void PlayRound()
        {
            Rule();
            UpdatePositions();
            BattleArea.UpdateArea();
        }

        private void LocalRule(int row, int col)
        {
            BattleField warrior = BattleArea.ActualBattleArea[row, col];
            if (warrior == BattleField.Walkable || warrior == BattleField.NotWalkable)
            {
                BattleArea.NextBattleArea[row, col] = BattleArea.ActualBattleArea[row, col];
                return;
            }
            BattleField oppositeWarrior = (warrior == BattleField.Greek)
                ? BattleField.Roman
                : BattleField.Greek;
            CountWarriorsv2(out int numberOfWarrior, out int numberOfOppositeWarrior, row, col, warrior, oppositeWarrior);
            LocalFight(numberOfWarrior, numberOfOppositeWarrior, row, col, warrior, oppositeWarrior);
        }

        private void CountWarriors(out int numberOfWarrior, out int numberOfOppositeWarrior,
            int row, int col, BattleField warrior, BattleField oppositeWarrior)
        {
            numberOfWarrior = 0;
            numberOfOppositeWarrior = 0;
            for (var i = row - 1; i <= row + 1; i++)
            {
                for (var j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < BattleArea.Length && j >= 0 && j < BattleArea.Width)
                    {
                        if (BattleArea.ActualBattleArea[i, j] == warrior)
                        {
                            numberOfWarrior++;
                        }
                        else if(BattleArea.ActualBattleArea[i, j] == oppositeWarrior)
                        {
                            numberOfOppositeWarrior++;
                        }
                    }
                }
            }

        }

        private void CountWarriorsv2(out int numberOfWarrior, out int numberOfOppositeWarrior,
           int row, int col, BattleField warrior, BattleField oppositeWarrior)
        {
            numberOfWarrior = 0;
            numberOfOppositeWarrior = 0;
            int a;
            int b;
            if(warrior == BattleField.Greek)
            {
                a = row;
                b = row + 1;
            }
            else
            {
                a = row - 1;
                b = row;
            }
            for (var i = a; i <= b; i++)
            {
                for (var j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < BattleArea.Length && j >= 0 && j < BattleArea.Width)
                    {
                        if (BattleArea.ActualBattleArea[i, j] == warrior)
                        {
                            numberOfWarrior++;
                        }
                        else if (BattleArea.ActualBattleArea[i, j] == oppositeWarrior)
                        {
                            numberOfOppositeWarrior++;
                        }
                    }
                }
            }

        }

        private void LocalFight(int numberOfWarrior, int numberOfOppositeWarrior, int row, int col,
            BattleField warrior, BattleField oppositeWarrior)
        {
            if (numberOfWarrior < numberOfOppositeWarrior)
            {
                BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
            }
            else if (numberOfWarrior == numberOfOppositeWarrior)
            {
                BattleArea.NextBattleArea[row, col] = (warrior > oppositeWarrior)
                    ? warrior
                    : BattleField.Walkable;
            }
        }

        private void Walk(int row, int col)
        {
            BattleField warrior = BattleArea.ActualBattleArea[row, col];
            if ((BattleArea.Ratio * row >= col) && (BattleArea.Width - BattleArea.Ratio * row >= col))
            {
                if (BattleArea.NextBattleArea[row, col + 1] == BattleField.Walkable)
                {
                    BattleArea.NextBattleArea[row, col + 1] = warrior;
                    BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                }

                else
                    BattleArea.NextBattleArea[row, col] = warrior;
            }
            else if ((BattleArea.Ratio * row < col) && (BattleArea.Width - BattleArea.Ratio * row > col))
            {
                if (BattleArea.NextBattleArea[row + 1, col] == BattleField.Walkable)
                {
                    BattleArea.NextBattleArea[row + 1, col] = warrior;
                    BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                }
                else
                    BattleArea.NextBattleArea[row, col] = warrior;
            }
            else if ((BattleArea.Ratio * row <= col) && (BattleArea.Width - BattleArea.Ratio * row <= col))
            {
                if (BattleArea.NextBattleArea[row, col - 1] == BattleField.Walkable)
                {
                    BattleArea.NextBattleArea[row, col - 1] = warrior;
                    BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                }
                else
                    BattleArea.NextBattleArea[row, col] = warrior;
            }
            else if ((BattleArea.Ratio * row > col) && (BattleArea.Width - BattleArea.Ratio * row < col))
            {
                if (BattleArea.NextBattleArea[row - 1, col] == BattleField.Walkable)
                {
                    BattleArea.NextBattleArea[row - 1, col] = warrior;
                    BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                }
                else
                    BattleArea.NextBattleArea[row, col] = warrior;
            }
        }

        private void Rule()
        {
            for (int i = 0; i < BattleArea.Length; i++)
            {
                for (int j = 0; j < BattleArea.Width; j++)
                {
                    LocalRule(i, j);
                }
            }
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < BattleArea.Length; i++)
            {
                for (int j = 0; j < BattleArea.Width; j++)
                {
                    if (BattleArea.NextBattleArea[i, j] == BattleField.Empty)
                        Walk(i, j);
                }
            }
        }
        
    }
}
