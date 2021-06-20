using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Data;

namespace Pathfinding.AStar
{
    public class AStarSearchService
    {
        private int step = 0;
        private List<string> visitedNodes;
        
        public void FindPath(string name)
        {
            visitedNodes = new List<string>();
            step = 0;
            var totalSegments = new List<Tuple<string, string, int>>();
            ExpandCurrentNode(name, totalSegments);

            Console.WriteLine("Solutia Finala: ");
            totalSegments.ForEach(x => Console.WriteLine($"Segment: {x.Item1} - {x.Item2}: {x.Item3}"));
            Console.WriteLine($"Distanta Totala: {totalSegments.Sum(x => x.Item3)}");
        }

        private void ExpandCurrentNode(string name, List<Tuple<string, string, int>> totalSegments)
        {
            // Expandam nodul - daca nu este nodul final ("Bucuresti")
            if(!name.Equals("Bucuresti"))
            {
                visitedNodes.Add(name);
                Console.WriteLine($"Stagiul {++step}: ");
                // Selectam vecinii nodului
                var neighboursEdges = Distances.Edges
                    .Where(x => x.Item1 == name || x.Item2 == name)
                    .ToList();
                
                var neighbours = neighboursEdges
                    .Select(x => x.Item1 == name ? x.Item2 : x.Item1)
                    .ToList();
                
                var neighboursDistanceToBucharest = Distances.StraightLineDistance
                    .Where(x => neighbours.Contains(x.Item1))
                    .ToList();

                // Verificam vecinii - distanta estimata de la pornire spre destinatie trecand prin nodul vecin f(n)= g(n) + h(n)
                var distanceSoFar = totalSegments.Sum(x => x.Item3);
                var neighboursEstimatedTotalDistance = neighboursEdges
                .Where(x => !visitedNodes.Contains(x.Item1) || !visitedNodes.Contains(x.Item2)) // nu includem noduri deja vizitate
                .Select(x => 
                {
                    var distanceToBucharest = neighboursDistanceToBucharest
                        .FirstOrDefault(y => y.Item1 == x.Item1 || y.Item1 == x.Item2);

                    return new Tuple<string, string, int, int>(x.Item1, x.Item2, x.Item3, x.Item3 + distanceToBucharest.Item2 + distanceSoFar);
                }).ToList();

                Console.WriteLine($"Vecini {name}: ");
                neighboursEstimatedTotalDistance.ForEach(x => Console.WriteLine($"{(name == x.Item1 ? x.Item2 : x.Item1)} - Distanta estimata prin nod spre Bucuresti: {x.Item4}"));

                // Alegem vecinul cu cea mai mica distanta estimata trecand prin nod
                var shortestDistanceToBucharest = neighboursEstimatedTotalDistance
                    .OrderBy(x => x.Item4)
                    .FirstOrDefault();

                var nextNodeName = name == shortestDistanceToBucharest.Item1 ? shortestDistanceToBucharest.Item2 : shortestDistanceToBucharest.Item1;
                Console.WriteLine($"Nodul selectat: {nextNodeName}");
                var currentSegment = new Tuple<string, string, int>(shortestDistanceToBucharest.Item1, shortestDistanceToBucharest.Item2, shortestDistanceToBucharest.Item3);
            
                // Adaugam vecinul la lista curenta a nodurilor cautate / lista segmentelor selectate
                totalSegments.Add(currentSegment);

                // Expandam noul nod - recursivitate
                ExpandCurrentNode(nextNodeName, totalSegments);
            }
            return;
        }
    }
}