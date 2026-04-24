using UniversityGraphAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityGraphAPI.Services
{
    public class GraphService
    {
        private readonly List<University> _nodes = new()
        {
            new() { Id = 1,  Name = "YSU - Yerevan State University",           Latitude = 40.1819, Longitude = 44.5265 },
            new() { Id = 2,  Name = "AUA - American University of Armenia",      Latitude = 40.1925, Longitude = 44.5042 },
            new() { Id = 3,  Name = "NPUA - Polytechnic University",             Latitude = 40.1878, Longitude = 44.5226 },
            new() { Id = 4,  Name = "ASUE - State University of Economics",      Latitude = 40.1872, Longitude = 44.5278 },
            new() { Id = 5,  Name = "YSMU - Medical University",                 Latitude = 40.1887, Longitude = 44.5262 },
            new() { Id = 6,  Name = "RAU - Russian-Armenian University",         Latitude = 40.2115, Longitude = 44.5028 },
            new() { Id = 7,  Name = "ANAU - Agrarian University",                Latitude = 40.1895, Longitude = 44.5245 },
            new() { Id = 8,  Name = "Brusov - State University of Languages",    Latitude = 40.1865, Longitude = 44.5101 },
            new() { Id = 9,  Name = "UFAR - French University in Armenia",       Latitude = 40.2101, Longitude = 44.5298 },
            new() { Id = 10, Name = "EUA - European University of Armenia",      Latitude = 40.1950, Longitude = 44.5270 },
            new() { Id = 11, Name = "YSAFA - Academy of Fine Arts",              Latitude = 40.1815, Longitude = 44.5185 }
        };

        private readonly List<Edge> _edges = new()
        {
            // YSU (1) Connections
            new() { Id = 1, FromId = 1, ToId = 2, DistanceKm = 2.4 }, new() { Id = 2, FromId = 1, ToId = 3, DistanceKm = 0.8 },
            new() { Id = 3, FromId = 1, ToId = 4, DistanceKm = 0.6 }, new() { Id = 4, FromId = 1, ToId = 5, DistanceKm = 0.7 },
            new() { Id = 5, FromId = 1, ToId = 6, DistanceKm = 4.2 }, new() { Id = 6, FromId = 1, ToId = 7, DistanceKm = 0.9 },
            new() { Id = 7, FromId = 1, ToId = 8, DistanceKm = 1.8 }, new() { Id = 8, FromId = 1, ToId = 9, DistanceKm = 5.1 },
            new() { Id = 9, FromId = 1, ToId = 10, DistanceKm = 3.8 }, new() { Id = 10, FromId = 1, ToId = 11, DistanceKm = 1.3 },

            // AUA (2) Connections
            new() { Id = 11, FromId = 2, ToId = 3, DistanceKm = 1.5 }, new() { Id = 12, FromId = 2, ToId = 4, DistanceKm = 2.1 },
            new() { Id = 13, FromId = 2, ToId = 5, DistanceKm = 1.9 }, new() { Id = 14, FromId = 2, ToId = 6, DistanceKm = 2.8 },
            new() { Id = 15, FromId = 2, ToId = 7, DistanceKm = 1.7 }, new() { Id = 16, FromId = 2, ToId = 8, DistanceKm = 0.9 },
            new() { Id = 17, FromId = 2, ToId = 9, DistanceKm = 3.5 }, new() { Id = 18, FromId = 2, ToId = 10, DistanceKm = 1.3 },
            new() { Id = 19, FromId = 2, ToId = 11, DistanceKm = 1.6 },

            // NPUA (3) Connections
            new() { Id = 20, FromId = 3, ToId = 4, DistanceKm = 0.4 }, new() { Id = 21, FromId = 3, ToId = 5, DistanceKm = 0.3 },
            new() { Id = 22, FromId = 3, ToId = 6, DistanceKm = 3.9 }, new() { Id = 23, FromId = 3, ToId = 7, DistanceKm = 0.2 },
            new() { Id = 24, FromId = 3, ToId = 8, DistanceKm = 1.4 }, new() { Id = 25, FromId = 3, ToId = 9, DistanceKm = 4.8 },
            new() { Id = 26, FromId = 3, ToId = 10, DistanceKm = 2.5 }, new() { Id = 27, FromId = 3, ToId = 11, DistanceKm = 1.1 },

            // ASUE (4) Connections
            new() { Id = 28, FromId = 4, ToId = 5, DistanceKm = 0.2 }, new() { Id = 29, FromId = 4, ToId = 6, DistanceKm = 4.4 },
            new() { Id = 30, FromId = 4, ToId = 7, DistanceKm = 0.3 }, new() { Id = 31, FromId = 4, ToId = 8, DistanceKm = 1.7 },
            new() { Id = 32, FromId = 4, ToId = 9, DistanceKm = 5.3 }, new() { Id = 33, FromId = 4, ToId = 10, DistanceKm = 3.2 },
            new() { Id = 34, FromId = 4, ToId = 11, DistanceKm = 1.0 },

            // YSMU (5) Connections
            new() { Id = 35, FromId = 5, ToId = 6, DistanceKm = 4.3 }, new() { Id = 36, FromId = 5, ToId = 7, DistanceKm = 0.2 },
            new() { Id = 37, FromId = 5, ToId = 8, DistanceKm = 1.6 }, new() { Id = 38, FromId = 5, ToId = 9, DistanceKm = 5.1 },
            new() { Id = 39, FromId = 5, ToId = 10, DistanceKm = 3.0 }, new() { Id = 40, FromId = 5, ToId = 11, DistanceKm = 1.1 },

            // RAU (6) Connections
            new() { Id = 41, FromId = 6, ToId = 7, DistanceKm = 4.1 }, new() { Id = 42, FromId = 6, ToId = 8, DistanceKm = 2.9 },
            new() { Id = 43, FromId = 6, ToId = 9, DistanceKm = 1.1 }, new() { Id = 44, FromId = 6, ToId = 10, DistanceKm = 1.6 },
            new() { Id = 45, FromId = 6, ToId = 11, DistanceKm = 4.3 },

            // ANAU (7) Connections
            new() { Id = 46, FromId = 7, ToId = 8, DistanceKm = 1.5 }, new() { Id = 47, FromId = 7, ToId = 9, DistanceKm = 5.0 },
            new() { Id = 48, FromId = 7, ToId = 10, DistanceKm = 2.8 }, new() { Id = 49, FromId = 7, ToId = 11, DistanceKm = 1.2 },

            // Brusov (8) Connections
            new() { Id = 50, FromId = 8, ToId = 9, DistanceKm = 3.8 }, new() { Id = 51, FromId = 8, ToId = 10, DistanceKm = 1.9 },
            new() { Id = 52, FromId = 8, ToId = 11, DistanceKm = 1.4 },

            // UFAR (9) Connections
            new() { Id = 53, FromId = 9, ToId = 10, DistanceKm = 2.4 }, new() { Id = 54, FromId = 9, ToId = 11, DistanceKm = 5.4 },

            // EUA (10) Connections
            new() { Id = 55, FromId = 10, ToId = 11, DistanceKm = 2.7 }
        };

        public List<University> GetAll() => _nodes;

        public List<University> GetUniversitiesOnly() => _nodes;

        public University GetNode(int id) => _nodes.FirstOrDefault(u => u.Id == id);

        public string GetName(int id) => _nodes.FirstOrDefault(u => u.Id == id)?.Name ?? "Unknown";

        public Dictionary<int, List<(int to, double weight)>> GetAdjacencyList()
        {
            var graph = new Dictionary<int, List<(int, double)>>();
            foreach (var u in _nodes)
                graph[u.Id] = new List<(int, double)>();

            foreach (var e in _edges)
            {
                // Ապահովում ենք երկկողմանի կապը
                graph[e.FromId].Add((e.ToId, e.DistanceKm));
                graph[e.ToId].Add((e.FromId, e.DistanceKm));
            }
            return graph;
        }
    }
}