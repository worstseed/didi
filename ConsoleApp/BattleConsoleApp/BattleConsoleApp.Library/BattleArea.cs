﻿namespace BattleConsoleApp.Library
{
    public class BattleArea
    {
        public BattleField[,] ActualBattleArea { get; set; }
        public BattleField[,] NextBattleArea { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public float Ratio { get; set; }

        // Konstruktor klasy, poza przypisaniem odpowiednich wartości do zmiennych, 
        //  wywołuje również metody odpowiedzialne za przygotowanie pola bitwy.
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


        // Poniższe metody tworzą “nieożywioną” przestrzeń pola bitwy 
        //  - ustalenie, po jakich komórkach pola bitwy jednostki mogą się poruszać, a po których nie.
        private void SetMap(BattleField[,] area)
        {
            CreateWalkableArea(area);
            SetTrees();
        }
        private void CreateWalkableArea(BattleField[,] area)
        {
            for (var i = 0; i < Length; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    area[i, j] = BattleField.Walkable;
                }
            }
<<<<<<< HEAD
            SetTrees(2);
=======
>>>>>>> bd6de9d1550792e7382025c29f179df1fe817d3b
        }
<<<<<<< HEAD

        private void MakeFormation()
        {
            SetRomanArmy();
            SetGaulsArmy();
<<<<<<< HEAD
=======
            SetTrees();
>>>>>>> bd6de9d1550792e7382025c29f179df1fe817d3b
        }

        private void SetTrees(int version = 1)
=======
        private void SetTrees(int version = 2)
>>>>>>> ed7452d1b0a3373256db70e353c02e0ce7f15c01
        {
            switch (version)
            {
                case 1:
                    for (var i = 0; i < Width; i = i + 2)
                    {
                        ActualBattleArea[5, i] = BattleField.NotWalkable;
                    }
                    break;
                case 2:
<<<<<<< HEAD
<<<<<<< HEAD
                    ActualBattleArea[5, 3] = BattleField.NotWalkable;
                    ActualBattleArea[5, 7] = BattleField.NotWalkable;
=======
                    //ActualBattleArea[3, 4] = BattleField.NotWalkable;
                    ActualBattleArea[4, 10] = BattleField.NotWalkable;
                    //ActualBattleArea[4, 13] = BattleField.NotWalkable;

                    ActualBattleArea[5, 1] = BattleField.NotWalkable;
                    //ActualBattleArea[5, 2] = BattleField.NotWalkable;
                    ActualBattleArea[5, 9] = BattleField.NotWalkable;
>>>>>>> ed7452d1b0a3373256db70e353c02e0ce7f15c01
                    ActualBattleArea[5, 10] = BattleField.NotWalkable;
                    ActualBattleArea[5, 13] = BattleField.NotWalkable;
                    ActualBattleArea[5, 17] = BattleField.NotWalkable;
                    ActualBattleArea[6, 5] = BattleField.NotWalkable;
<<<<<<< HEAD
                    ActualBattleArea[6, 11] = BattleField.NotWalkable;
                    ActualBattleArea[6, 15] = BattleField.NotWalkable;
=======
                    
>>>>>>> bd6de9d1550792e7382025c29f179df1fe817d3b
=======
                    //ActualBattleArea[6, 11] = BattleField.NotWalkable;
                    ActualBattleArea[6, 15] = BattleField.NotWalkable;
>>>>>>> ed7452d1b0a3373256db70e353c02e0ce7f15c01
                    break;
            }
        }
        
        // Poniższe metody tworzą “ożywioną” przestrzeń pola bitwy 
        //  - ustalenie startowych formacji oraz liczebności wojsk.
        private void MakeFormation()
        {
            SetRomanArmy();
            SetGaulsArmy();
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

        // Poniższe metody potrzebne są ze względów programistycznych 
        //  - przejście między jedną reprezentacją pola bitwy do drugiej.
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
