using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleConsoleApp.Library;

namespace BattleConsoleApp
{
    static class Display
    {
        static public void WriteToConsole(BattleArea area)
        {
            for(int i = 0; i< area.Length; i++)
            {
                for(int j = 0; j < area.Width; j++)
                {
                    BattleField field = area.ActualBattleArea[i, j];
                    switch (field)
                    {
                        case BattleField.Walkable:
                            Console.Write('-');
                            break;
                        case BattleField.NotWalkable:
                            Console.Write('n');
                            break;
                        case BattleField.Greek:
                            Console.Write('g');
                            break;
                        case BattleField.Roman:
                            Console.Write('r');
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
