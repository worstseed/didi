using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var counterRow = 0;
            var counterCol = 0;
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Console.Write(counterRow);
                    Console.Write(", ");
                    Console.Write(counterCol);
                    Console.Write("       ");
                    counterCol++;
                }
                Console.WriteLine();
                counterRow++;
                counterCol = 0;
            }
        }
    }
}
