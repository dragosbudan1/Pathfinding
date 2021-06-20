using Pathfinding.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Greedy
{
    public class GreedySearchService
    {
        private int step = 0;
        public GreedySearchService()
        {

        }

        public void FindPath(string start)
        {
            step = 0;
            var totalSegments = new List<Tuple<string, string, int>>();
            
            ExpandCurrentNode(start, totalSegments);

            Console.WriteLine("Solutia Finala: ");
            totalSegments.ForEach(x => Console.WriteLine($"Segment: {x.Item1} - {x.Item2}: {x.Item3}"));
            Console.WriteLine($"Distanta Totala: {totalSegments.Sum(x => x.Item3)}");
        }

        private void ExpandCurrentNode(string name, List<Tuple<string, string, int>> totalSegments)
        {
            // Expandam nodul - daca nu este nodul final ("Bucuresti")
            if(!name.Equals("Bucuresti"))
            {
                Console.WriteLine($"Stagiul {++step}: ");
                // Selectam vecinii nodului
                var neighbours = Distances.Edges
                    .Where(x => x.Item1 == name || x.Item2 == name)
                    .Select(x => x.Item1 == name ? x.Item2 : x.Item1)
                    .ToList();
                
                // Alegem vecinul cel apropriat de Bucuresti
                var neighboursDistanceToBucharest = Distances.StraightLineDistance
                    .Where(x => neighbours.Contains(x.Item1))
                    .ToList();

                Console.WriteLine($"Vecini {name}: ");
                neighboursDistanceToBucharest.ForEach(x => Console.WriteLine($"{x.Item1} - Distanta directa Bucuresti: {x.Item2}"));

                var shortestDistanceToBucharest = neighboursDistanceToBucharest
                    .OrderBy(x => x.Item2)
                    .FirstOrDefault();

                Console.WriteLine($"Nodul selectat: {shortestDistanceToBucharest.Item1}");

                var currentSegment = Distances.Edges
                    .Where(x => (x.Item1 == name || x.Item1 == shortestDistanceToBucharest.Item1) && (x.Item2 == name || x.Item2 == shortestDistanceToBucharest.Item1))
                    .FirstOrDefault();
            
                // Adaugam vecinul la lista curenta a nodurilor cautate / lista segmentelor selectate
                totalSegments.Add(currentSegment);

                // Expandam noul nod - recursivitate
                ExpandCurrentNode(shortestDistanceToBucharest.Item1, totalSegments);
            }
            return;
        }
    }
}