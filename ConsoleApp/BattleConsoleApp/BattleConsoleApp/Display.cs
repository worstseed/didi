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
        public static void WriteToConsole(BattleArea area)
        {
            for(var i = 0; i< area.Length; i++)
            {
                for(var j = 0; j < area.Width; j++)
                {
                    BattleField field = area.ActualBattleArea[i, j];
                    switch (field)
                    {
                        case BattleField.Walkable:
                            Console.Write('-');
                            break;
                        case BattleField.NotWalkable:
                            Console.Write('x');
                            break;
                        case BattleField.Roman:
                            Console.Write('^');
                            break;
                        case BattleField.Gauls:
                            Console.Write('*');
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
