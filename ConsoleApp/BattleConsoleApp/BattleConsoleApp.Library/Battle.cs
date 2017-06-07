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
            BattleField oppositeWarrior = (warrior == BattleField.Roman)
                ? BattleField.Gauls
                : BattleField.Roman;
            CountWarriors(out int numberOfWarrior, out int numberOfOppositeWarrior, row, col, warrior, oppositeWarrior);
            LocalFight(numberOfWarrior, numberOfOppositeWarrior, row, col, warrior, oppositeWarrior);
        }

        private void CountWarriors(out int numberOfWarrior, out int numberOfOppositeWarrior,
            int row, int col, BattleField warrior, BattleField oppositeWarrior, int version = 2)
        {
            numberOfWarrior = 0;
            numberOfOppositeWarrior = 0;
            switch (version)
            {
                case 1:
                    CountWarriors_v1(ref numberOfWarrior, ref numberOfOppositeWarrior, row, col, warrior, oppositeWarrior);
                    break;
                case 2:
                    CountWarriors_v2(ref numberOfWarrior, ref numberOfOppositeWarrior, row, col, warrior, oppositeWarrior);
                    break;
            }

        }

        private void CountWarriors_v1(ref int numberOfWarrior, ref int numberOfOppositeWarrior, int row, int col,
            BattleField warrior, BattleField oppositeWarrior)
        {
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
                        else if (BattleArea.ActualBattleArea[i, j] == oppositeWarrior)
                        {
                            numberOfOppositeWarrior++;
                        }
                    }
                }
            }
        }

        private void CountWarriors_v2(ref int numberOfWarrior, ref int numberOfOppositeWarrior, int row, int col,
            BattleField warrior, BattleField oppositeWarrior)
        {
            int a;
            int b;
            if (warrior == BattleField.Roman)
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
                    if (i < 0 || i >= BattleArea.Length || j < 0 || j >= BattleArea.Width) continue;
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

        private void Walk(int row, int col, int version = 2)
        {
            BattleField warrior = BattleArea.ActualBattleArea[row, col];
            switch (version)
            {
                case 1:
                    Move_v1(row, col, warrior);
                    break;
                case 2:
                    Move_v2(row, col, warrior);
                    break;
            }
        }

        private void Move_v2(int row, int col, BattleField warrior)
        {
            if (row < BattleArea.Length / 2 - 1)
            {
                switch (BattleArea.NextBattleArea[row + 1, col])
                {
                    case BattleField.Walkable:
                        BattleArea.NextBattleArea[row + 1, col] = warrior;
                        BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                        break;
                    case BattleField.NotWalkable:
                        MoveAroundObstacle(row, col, warrior);
                        break;
                    default:
                        BattleArea.NextBattleArea[row, col] = warrior;
                        break;
                }
            }
            else if (row > BattleArea.Length / 2 )
            {
                switch (BattleArea.NextBattleArea[row - 1, col])
                {
                    case BattleField.Walkable:
                        BattleArea.NextBattleArea[row - 1, col] = warrior;
                        BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                        break;
                    case BattleField.NotWalkable:
                        MoveAroundObstacle(row, col, warrior);
                        break;
                    default:
                        BattleArea.NextBattleArea[row, col] = warrior;
                        break;
                }
            }
            else if(col < BattleArea.Width / 2)
            {
                if(BattleArea.NextBattleArea[row, col + 1] == BattleField.Walkable)
                {
                    BattleArea.NextBattleArea[row, col + 1] = warrior;
                    BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                }
                else if (BattleArea.NextBattleArea[row, col + 1] == BattleField.NotWalkable)
                    MoveAroundObstacle(row, col, warrior);
                else
                    BattleArea.NextBattleArea[row, col] = warrior;
            }
            else if (col >= BattleArea.Width / 2)
            {
                if (BattleArea.NextBattleArea[row, col - 1] == BattleField.Walkable)
                {
                    BattleArea.NextBattleArea[row, col - 1] = warrior;
                    BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
                }
                else if (BattleArea.NextBattleArea[row, col - 1] == BattleField.NotWalkable)
                    MoveAroundObstacle(row, col, warrior);
                else
                    BattleArea.NextBattleArea[row, col] = warrior;
            }
        }

        private void MoveAroundObstacle(int row, int col, BattleField warrior)
        {
            if (col - 1 > -1 && BattleArea.NextBattleArea[row, col - 1] == BattleField.Walkable)
            {
                BattleArea.NextBattleArea[row, col - 1] = warrior;
                BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
            }
            else if (col + 1 < BattleArea.Width && BattleArea.NextBattleArea[row, col + 1] ==
                     BattleField.Walkable)
            {
                BattleArea.NextBattleArea[row, col + 1] = warrior;
                BattleArea.NextBattleArea[row, col] = BattleField.Walkable;
            }
            else
            {
                BattleArea.NextBattleArea[row, col] = warrior;
            }
        }

        private void Move_v1(int row, int col, BattleField warrior)
        {
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
            for (var i = 0; i < BattleArea.Length; i++)
            {
                for (var j = 0; j < BattleArea.Width; j++)
                {
                    LocalRule(i, j);
                }
            }
        }

        private void UpdatePositions()
        {
            for (var i = BattleArea.Length / 2 - 1; i > -1; i--)
            {
                for (var j = 0; j < BattleArea.Width; j++)
                {
                    if (BattleArea.NextBattleArea[i, j] == BattleField.Empty)
                        Walk(i, j);
                }
            }
            for (var i = BattleArea.Length / 2; i < BattleArea.Length; i++)
            {
                for (var j = 0; j < BattleArea.Width; j++)
                {
                    if (BattleArea.NextBattleArea[i, j] == BattleField.Empty)
                        Walk(i, j);
                }
            }
        }
        
    }
}
