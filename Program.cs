using System;
using LatinSquare;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {

            LatinSquare.Graph graph = new Graph();

            graph.Print();

            Console.WriteLine("Enter values: ");
            string[] tokens = Console.ReadLine().Split(' ');

            Array.ForEach(tokens, x => Console.WriteLine(x));

            graph.Update(tokens);

            

            graph.Print();

            graph.AssignColor();

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

    }
}
