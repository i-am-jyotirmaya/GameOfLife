using System;
using System.Collections.Generic;
using GameUtilities;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cell> inputCoordinates = new List<Cell>();
            int x, y;

            Console.WriteLine("Provide coordinates of cells (separated by comma): (Enter 0 to stop input)");
            string input = "";
            do
            {
                input = Console.ReadLine();
                if (input == "0") continue;
                if(!input.Contains(","))
                {
                    Console.Error.WriteLine("Kindly provide inputs separated by comma (,) !");
                    continue;
                }
                string[] coords = input.Split(",");
                try
                {
                    x = Convert.ToInt32(coords[0]);
                    y = Convert.ToInt32(coords[1]);

                    inputCoordinates.Add(new Cell(x, y));
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("Kindly provide only numeric values!");
                }
                catch(Exception)
                {
                    Console.Error.WriteLine("Invalid input! Try Again.");
                }

            }
            while (input != "0");

            Game game = new Game();
            game.SetInitialState(inputCoordinates.ToArray());

            Console.Clear();

            Console.WriteLine("Initial State\n***************");
            Console.WriteLine(game.getCurrentState());
            Console.WriteLine("\n");
            game.ProceedTicks(1);
            Console.WriteLine("Result\n***************");
            Console.WriteLine(game.getCurrentState());
        }
    }
}
