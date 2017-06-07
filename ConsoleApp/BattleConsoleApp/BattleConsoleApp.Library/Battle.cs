namespace BattleConsoleApp.Library
{
    public class Battle
    {
        public BattleArea BattleArea { get; set; }

        // Konstruktor klasy - stworzenie pola bitwy o zadanym rozmiarze.
        public Battle()
        {
            BattleArea = new BattleArea(10, 20);
        }

        // Metoda przeprowadza pole bitwy do następnego stanu:
        //   1) Wywołanie funkcji przejścia dla całego automatu 
        //       - w pierwszym kroku jednostki ze sobą walczą
        //   2) Aktualiazcja pozycji
        //       - w drugim jednostki wykonują ruch.
        public void NextTurn()
        {
            TransitionRule();
            UpdatePositions();
            BattleArea.UpdateArea();
        }

        // Funkcja przejścia - wykonanie dla każdej komórki reguły lokalnej.
        private void TransitionRule()
        {
            for (var i = 0; i < BattleArea.Length; i++)
            {
                for (var j = 0; j < BattleArea.Width; j++)
                {
                    LocalRule(i, j);
                }
            }
        }

        // Reguła lokalna:
        //  1) Sprawdzenie czy rozpatrywana komórka jest jednostką armii
        //  2) Jeżeli tak - sprawdzenie, do której armii należy
        //  3) Zliczenie jednostek swojej armii oraz przeciwnej w sąsiedztwie
        //  4) Symulacja walki.
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

        // Metody oraz wersje odpowiedzialne za zliczenie jednostek w sąsiedztwie:
        //  _v1 - sąsiedztwo Moore'a
        //  _v2 - sąsiedztwo Moore'a "bez pleców"
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

        // Metoda, która na podstawie liczby jednostek swojej oraz przeciwnej armii w sąsiedztwie
        //  określa stan rozpatrywnej komórki w nastęnej turze
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

        // Metoda ta symuluje ruch wojsk. Bierzemy pod uwagę położenie jednostek i nie przechodzimy
        //  w prosty (od góry do dołu, od lewej do prawej) sposób po całym polu bitwy, żeby nie faworyzować 
        //   żadnej armii.
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

        // Metoda decydująca o przemieszczeniu się pojedynczej jednostki:
        //  _v1 - poruszanie się po sektorach wyznaczonych przez przekątne prostokąta
        //  _v2 - poruszanie się w stronę przeciwnika, aż do osiągnięcia pasma środkowego
        //         pola bitwy, gdzie jednostki poruszają się do środka geometrycznego pola bitwy
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

        // Metoda decydująca o przemieszczeniu w przypadku natrafienia na przeszkodę
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
    }
}