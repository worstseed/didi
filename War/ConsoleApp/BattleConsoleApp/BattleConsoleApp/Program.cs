using System;
using BattleConsoleApp.Library;

namespace BattleConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            var battle = new Battle();
            var numberOfRounds = 30;
            for (var i = 1; i <= numberOfRounds; i++)
            {
                Display.WriteToConsole(battle.BattleArea);
                battle.NextTurn();
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
