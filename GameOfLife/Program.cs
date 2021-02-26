using System;
using GameUtilities;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.SetInitialState(new Cell[] {
                new Cell(1,1),
                new Cell(1,0),
                new Cell(1,2),
            });

            Console.WriteLine(game.getCurrentState());

            game.ProceedTicks(1);
            Console.WriteLine("Result");
            Console.WriteLine(game.getCurrentState());
        }
    }
}
