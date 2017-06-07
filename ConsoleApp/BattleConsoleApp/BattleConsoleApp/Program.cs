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
        private static void Main(string[] args)
        {
            var battle = new Battle();
            var numberOfRounds = 15;
            for (var i = 1; i <= numberOfRounds; i++)
            {
                Display.WriteToConsole(battle.BattleArea);
                battle.PlayRound();
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
