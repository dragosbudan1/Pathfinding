using System;
using System.Linq;
using Pathfinding.Data;
using Pathfinding.Greedy;
using Pathfinding.AStar;

namespace Pathfinding
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mihnea Budan - Licenta 2021");
            var greedySearchService = new GreedySearchService();
            var aStarSearchService = new AStarSearchService();
            var running = true;

            while(running)
            {
                Console.WriteLine("Alegeti orasul de pornire: ");
                var input = Console.ReadLine();

                // Iesire program       
                if(input == "q")
                {
                    running = false;
                    continue;
                }

                if(!Distances.StraightLineDistance.Any(x => x.Item1 == input))
                {
                    Console.WriteLine($"Nume {input} incorect. Va rugam incercati din nou...");
                    continue;
                }

                Console.WriteLine("######## G R E E D Y ########");
                greedySearchService.FindPath(input);
                Console.WriteLine("######## F I N A L - G R E E D Y ########");
                Console.WriteLine();

                Console.WriteLine("######## A* ########");
                aStarSearchService.FindPath(input);
                Console.WriteLine("######## F I N A L - A* ########");
            } 

            //greedySearchService.FindPath("Timisoara");
            //aStarSearchService.FindPath("Arad");         
        }
    }
}
