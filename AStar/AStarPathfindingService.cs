using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Data;

namespace Pathfinding.AStar
{
    public class QueueNode 
    {
        public string Name { get; set; }
        public List<string> PreviousNodes { get; set; }
    }

    public class AStarSearchService
    {
        private PriorityQueue<QueueNode, int> _openNodeList;
        private List<QueueNode> _visitedNodeList;

        private List<Tuple<string, string, int>> _currentSolution;
        private List<string> _solutionNodeList;

        private int _step;

        public AStarSearchService()
        {
            _openNodeList = new PriorityQueue<QueueNode, int>();
            _visitedNodeList = new List<QueueNode>();
            _currentSolution = new List<Tuple<string, string, int>>();
            _step = 0;
        }
        
        public void FindPath(string name)
        {
            _openNodeList = new PriorityQueue<QueueNode, int>();
            _visitedNodeList = new List<QueueNode>();
            _currentSolution = new List<Tuple<string, string, int>>();
            _solutionNodeList = new List<string>();
            _step = 0;

            // Punctul de pornire - adaugam nodul de pornire la lista nodurilor ce trebuie expandate
            var startNode = Distances.StraightLineDistance.FirstOrDefault(x => x.Item1 == name);
            if(startNode == null) return;

            var startQueueNode = new QueueNode {Name = startNode.Item1, PreviousNodes = null};
            _openNodeList.Enqueue(startQueueNode, startNode.Item2);

            ExpandCurrentNode();

            if(_solutionNodeList != null && _solutionNodeList.Count > 1) 
            {
                var totalEdges = new List<Tuple<string, string, int>>();
                for(int i = 0; i < _solutionNodeList.Count - 1; i++)
                {
                    var name1 = _solutionNodeList[i];
                    var name2 = _solutionNodeList[i + 1];

                    var edge = Distances.Edges
                        .FirstOrDefault(x => (x.Item1 == name1 || x.Item1 == name2) && (x.Item2 == name1 || x.Item2 == name2));
                    
                    totalEdges.Add(edge);
                }

                Console.WriteLine("Solutia Finala: ");
                totalEdges.ForEach(x => Console.WriteLine($"Segment {x.Item1} - {x.Item2}: Distanta {x.Item3}"));
                Console.WriteLine($"Distanta totala: {totalEdges.Sum(x => x.Item3)}");
            }
         
        }

        private void ExpandCurrentNode()
        {
            // Selectam varful cozii
            var queueTop = _openNodeList.Peek();

            Console.WriteLine("##### COADA CURENTA #####");
            var printList = _openNodeList.UnorderedItems.OrderBy(x => x.Priority).ToList();
            foreach(var node in printList)
            {
                var previousNodeString = new String("");
                if(node.Element.PreviousNodes != null && node.Element.PreviousNodes.Any())
                {
                for(int i = 0; i < node.Element.PreviousNodes.Count; i++)
                    {
                        previousNodeString += node.Element.PreviousNodes[i];
                        if(i != node.Element.PreviousNodes.Count - 1) 
                        {
                        
                            previousNodeString += ", ";
                        }
                    }
                }

                Console.WriteLine($"Nod: {node.Element.Name}, Noduri anterioare: {previousNodeString}, Distanta provizorie: {node.Priority}");
            }
            Console.WriteLine("##### SFARSIT COADA CURENTA #####");

            // Daca varful cozii este Bucuresti cautarea s-a incheiat
            if(queueTop.Name == "Bucuresti")
            {
                var solutionNodeList = new List<string>();
                if(queueTop.PreviousNodes != null)
                {
                    solutionNodeList.AddRange(queueTop.PreviousNodes);
                }
                solutionNodeList.Add(queueTop.Name);
                _solutionNodeList = solutionNodeList;
                return;
            }

            Console.WriteLine($"Stagiul {++_step}: ");
            Console.WriteLine($"Expandam nodul: {queueTop.Name}");

            // adaugam la lista nodurilor vizitate
            _visitedNodeList.Add(queueTop);

            // Gasim vecinii si costurile asociate
            var neighbours = Distances.Edges
                .Where(x => x.Item1 == queueTop.Name || x.Item2 == queueTop.Name)
                .ToList();
            

            var distanceSoFar = GetDistanceFromQueueTop(queueTop);
            var neighboursAssociatedCosts = neighbours
                .Select(x => 
                {
                    var neighbourName = queueTop.Name == x.Item1 ? x.Item2 : x.Item1;
                    var straightLineToBucharest = Distances.StraightLineDistance
                        .FirstOrDefault(x => x.Item1 == neighbourName); 

                    var neighbourTuple = new Tuple<string, string, int, int>(x.Item1, x.Item2, x.Item3, x.Item3 + straightLineToBucharest.Item2 + distanceSoFar);

                    Console.WriteLine($"{neighbourTuple.Item1} - {neighbourTuple.Item2}: Distanta Totala provizorie: {neighbourTuple.Item4}");

                    return neighbourTuple;
                }).ToList();
            
            // Adaugam vecinii expandati la coada deschisa (incluzand costurile asociate)
            neighboursAssociatedCosts.ForEach(x => 
            {
                var neighbourName = queueTop.Name == x.Item1 ? x.Item2 : x.Item1;

                var previousNodes = new List<string>();
                if(queueTop.PreviousNodes != null && queueTop.PreviousNodes.Count > 0) 
                {
                    previousNodes.AddRange(queueTop.PreviousNodes);
                }
                previousNodes.Add(queueTop.Name);

                var newQueueNode = new QueueNode { Name = neighbourName, PreviousNodes = previousNodes };
                _openNodeList.Enqueue(newQueueNode, x.Item4);
            });

            // Scoatem varful cozii
            _openNodeList.Dequeue();

            ExpandCurrentNode();
        }

        private int GetDistanceFromQueueTop(QueueNode queueTop)
        {
            var nodeList = new List<string>();

            if(queueTop.PreviousNodes != null)
            {
                nodeList.AddRange(queueTop.PreviousNodes);
            }

            nodeList.Add(queueTop.Name);

            if(nodeList != null && nodeList.Count > 1) 
            {
                var currentEdgesDistance = 0;
                for(int i = 0; i < nodeList.Count - 1; i++)
                {
                    var name1 = nodeList[i];
                    var name2 = nodeList[i + 1];

                    var edge = Distances.Edges
                        .FirstOrDefault(x => (x.Item1 == name1 || x.Item1 == name2) && (x.Item2 == name1 || x.Item2 == name2));
                    
                    if(edge != null)
                    {
                        currentEdgesDistance += edge.Item3;
                    }
                }

                return currentEdgesDistance;
            }

            return 0;
        }
    }
}