namespace BattleConsoleApp.Library
{
    public class BattleArea
    {
        public BattleField[,] ActualBattleArea { get; set; }
        public BattleField[,] NextBattleArea { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public float Ratio { get; set; }

        public BattleArea(int length, int width)
        {
            Length = length;
            Width = width;
            ActualBattleArea = new BattleField[Length, Width];
            NextBattleArea = new BattleField[Length, Width];
            Ratio = (float)Width/ Length;
            SetMap(ActualBattleArea);
            EmptyArea(NextBattleArea);
            MakeFormation();
        }

        private void SetMap(BattleField[, ] area)
        {
            for(var i = 0; i < Length; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    area[i, j] = BattleField.Walkable;
                }
            }
        }

        private void MakeFormation()
        {
            SetRomanArmy();
            SetGaulsArmy();
            SetTrees();
        }

        private void SetTrees()
        {
            for (var i = 0; i < Width; i = i + 2)
            {
                ActualBattleArea[5, i] = BattleField.NotWalkable;
            }
            
        }

        private void SetRomanArmy()
        {
        // Roman army.
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    ActualBattleArea[j, i] = BattleField.Roman;
                }
            }
        }

        private void SetGaulsArmy()
        {
            // Gauls army.
            for (var i = 0; i < Width; i++)
            {
                for (var j = Length - 2; j < Length; j++)
                {
                    ActualBattleArea[j, i] = BattleField.Gauls;
                }
            }
        }

        public void UpdateArea()
        {
            for(var i = 0; i < Length; i++)
            {
                for(var j = 0; j < Width; j++)
                {
                    ActualBattleArea[i,j] = NextBattleArea[i, j];
                }
            }
            EmptyArea(NextBattleArea);
        }
        
        private void EmptyArea(BattleField[,] area)
        {
            for (var i = 0; i < Length; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    area[i, j] = BattleField.Empty;
                }
            }
        }
    }
}
