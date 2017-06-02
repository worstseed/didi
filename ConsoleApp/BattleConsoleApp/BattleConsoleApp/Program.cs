using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleConsoleApp.Library;

namespace BattleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Battle _battle = new Battle();
            int numberOfRounds = 30;
            for (int i = 1; i <= numberOfRounds; i++)
            {
                Display.WriteToConsole(_battle.BattleArea);
                _battle.PlayRound();
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
