using System;

namespace Pathfinding.Data
{
    public static class Distances
    {
        public static Tuple<string, string, int>[] Edges = 
        {
            new Tuple<string, string, int>("Arad", "Sibiu", 140),
            new Tuple<string, string, int>("Arad", "Zerind", 75),
            new Tuple<string, string, int>("Arad", "Timisoara", 118),
            new Tuple<string, string, int>("Zerind", "Oradea", 71),
            new Tuple<string, string, int>("Oradea", "Sibiu", 151),
            new Tuple<string, string, int>("Timisoara", "Lugoj", 111),
            new Tuple<string, string, int>("Sibiu", "Fagaras", 99),
            new Tuple<string, string, int>("Sibiu", "Ramnicu-Valcea", 80),
            new Tuple<string, string, int>("Lugoj", "Mehadia", 70),
            new Tuple<string, string, int>("Fagaras", "Bucuresti", 211),
            new Tuple<string, string, int>("Ramnicu-Valcea", "Pitesti", 97),
            new Tuple<string, string, int>("Ramnicu-Valcea", "Craiova", 146),
            new Tuple<string, string, int>("Mehadia", "Drobeta", 75),
            new Tuple<string, string, int>("Bucuresti", "Pitesti", 101),
            new Tuple<string, string, int>("Bucuresti", "Urziceni", 85),
            new Tuple<string, string, int>("Bucuresti", "Giurgiu", 90),
            new Tuple<string, string, int>("Pitesti", "Craiova", 138),
            new Tuple<string, string, int>("Craiova", "Drobeta", 120),
            new Tuple<string, string, int>("Urziceni", "Harsova", 98),
            new Tuple<string, string, int>("Urziceni", "Vaslui", 142),
            new Tuple<string, string, int>("Harsova", "Eforie", 86),
            new Tuple<string, string, int>("Vaslui", "Iasi", 92),
            new Tuple<string, string, int>("Iasi", "Neamt", 87),
        };

        public static Tuple<string, int>[] StraightLineDistance = 
        {
            new Tuple<string, int>("Arad", 366),
            new Tuple<string, int>("Bucuresti", 0),
            new Tuple<string, int>("Craiova", 160),
            new Tuple<string, int>("Drobeta", 242),
            new Tuple<string, int>("Eforie", 161),
            new Tuple<string, int>("Fagaras", 176),
            new Tuple<string, int>("Giurgiu", 77),
            new Tuple<string, int>("Harsova", 151),
            new Tuple<string, int>("Iasi", 226),
            new Tuple<string, int>("Lugoj", 244),
            new Tuple<string, int>("Mehadia", 241),
            new Tuple<string, int>("Neamt", 234),
            new Tuple<string, int>("Oradea", 380),
            new Tuple<string, int>("Pitesti", 98),
            new Tuple<string, int>("Ramnicu-Valcea", 193),
            new Tuple<string, int>("Sibiu", 253),
            new Tuple<string, int>("Timisoara", 329),
            new Tuple<string, int>("Urziceni", 80),
            new Tuple<string, int>("Vaslui", 199),
            new Tuple<string, int>("Zerind", 374),
        };
    }
}

// Arad, 366
// Bucuresti, 0
// Craiova, 160
// Dobreta, 242
// Eforie, 161
// Fagaras, 176
// Giurgiu, 77
// Hirsowa, 151
// Lasi, 226
// Lugoj, 244
// Mehadia, 241
// Neamt, 234
// Oradea, 380
// Pitesti, 100
// Rimnicu Vilcea, 193
// Sibiu, 253
// Timisoara, 329
// Urziceni, 80
// Vaslui, 199
// Zerind, 374

// Arad, Zerind, 75
// Arad, Sibiu, 140
// Arad, Timisoara, 118
// Zerind, Oradea, 71
// Oradea, Sibiu, 151
// Timisoara, Lugoj, 111
// Sibiu, Fagaras, 99
// Sibiu, Rimnicu Vilcea, 80
// Lugoj, Mehadia, 70
// Fagaras, Bucuresti, 211
// Rimnicu Vilcea, Pitesti, 97
// Rimnicu Vilcea, Craiova, 146
// Mehadia, Dobreta, 75
// Bucuresti, Pitesti, 101
// Bucuresti, Urziceni, 85
// Bucuresti, Giurglu, 90
// Pitesti, Craiova, 138
// Craiova, Dobreta, 120
// Urziceni, Hirsova, 98
// Urziceni, Vaslui, 142
// Hirsova, Eforie, 86
// Vaslui, Lasi, 92
// Lasi, Neamt, 87