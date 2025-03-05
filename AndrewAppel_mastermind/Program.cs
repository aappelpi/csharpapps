// See https://aka.ms/new-console-template for more information
using System;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;

namespace AndrewAppel_mastermind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            var currentDate = DateTime.Now;
            Console.WriteLine($"{Environment.NewLine}Hello, {name}, on {currentDate:d} at {currentDate:t}!");



            Game game = new Game();

            string response = string.Empty;

            int turnNumber = 1;
            while (turnNumber <= game.maxNumberOfTurns)
            {

                response = game.TakeATurn(turnNumber);

                Console.WriteLine($"Result: {response}");

                if (!response.Contains("Invalid"))
                {
                    turnNumber++;
                }
                if (response.Contains("CORRECT"))
                {
                    break;
                }
            }
            if (turnNumber > game.maxNumberOfTurns)
            {
                Console.Write($"did not succeed in {game.maxNumberOfTurns} turns.  secret was: {string.Concat(game.HiddenSolution.Select(x => x.ToString()))}");
            }
            Console.Write($"{Environment.NewLine}Press Enter to exit...");
            Console.Read();
        }

    }



}

