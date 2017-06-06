using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SetGreekArmy();
            SetRomanArmy();
        }

        private void SetGreekArmy()
        {
        // Greek army xd.
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    ActualBattleArea[j, i] = BattleField.Greek;
                }
            }
        }

        private void SetRomanArmy()
        {
            // Roman army xd.
            for (var i = 0; i < Width; i++)
            {
                for (var j = Length - 2; j < Length; j++)
                {
                    ActualBattleArea[j, i] = BattleField.Roman;
                }
            }
        }

        public void UpdateArea()
        {
            for(int i = 0; i < Length; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    ActualBattleArea[i,j] = NextBattleArea[i, j];
                }
            }
            EmptyArea(NextBattleArea);
        }
        
        private void EmptyArea(BattleField[,] area)
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    area[i, j] = BattleField.Empty;
                }
            }
        }
    }
}
