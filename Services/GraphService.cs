using UniversityGraphAPI.Models;

namespace UniversityGraphAPI.Services
{
    public class GraphService
    {
        private readonly List<University> _nodes = new()
        {
            // 10 Հայկական համալսարաններ (անգլերեն)
            new() { Id = 1,  Name = "YSU - Yerevan State University",           Latitude = 40.1819, Longitude = 44.5151 },
            new() { Id = 2,  Name = "AUA - American University of Armenia",      Latitude = 40.1872, Longitude = 44.5154 },
            new() { Id = 3,  Name = "NPUA - Polytechnic University",             Latitude = 40.1734, Longitude = 44.5081 },
            new() { Id = 4,  Name = "ASUE - State University of Economics",      Latitude = 40.2012, Longitude = 44.5234 },
            new() { Id = 5,  Name = "YSMU - Medical University",                 Latitude = 40.1923, Longitude = 44.5187 },
            new() { Id = 6,  Name = "RAU - Russian-Armenian University",         Latitude = 40.2031, Longitude = 44.5189 },
            new() { Id = 7,  Name = "ANAU - Agrarian University",                Latitude = 40.1801, Longitude = 44.5343 },
            new() { Id = 8,  Name = "Brusov - State University of Languages",    Latitude = 40.1856, Longitude = 44.5201 },
            new() { Id = 9,  Name = "GAA - Gladzor Academy of Art",              Latitude = 40.1790, Longitude = 44.5110 },
            new() { Id = 10, Name = "EUAM - European University of Armenia",     Latitude = 40.1950, Longitude = 44.5270 },

            // Իրական վայրեր — հանգույցներ
            new() { Id = 11, Name = "Metronom",          Latitude = 40.1841, Longitude = 44.5155 },
            new() { Id = 12, Name = "Cascade",           Latitude = 40.1888, Longitude = 44.5092 },
            new() { Id = 13, Name = "Republic Square",   Latitude = 40.1777, Longitude = 44.5126 },
            new() { Id = 14, Name = "Opera",             Latitude = 40.1874, Longitude = 44.5137 },
            new() { Id = 15, Name = "Kyevyan Bridge",    Latitude = 40.1811, Longitude = 44.5085 },
            new() { Id = 16, Name = "Mashtots Avenue",   Latitude = 40.1820, Longitude = 44.5130 },
            new() { Id = 17, Name = "Baghramyan Ave",    Latitude = 40.1900, Longitude = 44.5140 },
            new() { Id = 18, Name = "Tigranakert",       Latitude = 40.1760, Longitude = 44.5200 },
            new() { Id = 19, Name = "Zoravar Andranik",  Latitude = 40.2000, Longitude = 44.5160 },
            new() { Id = 20, Name = "Komitas Junction",  Latitude = 40.2020, Longitude = 44.5210 },
        };

        private readonly List<Edge> _edges = new()
        {
            // YSU connections
            new() { FromId = 1,  ToId = 11, DistanceKm = 0.4 },  // YSU → Metronom
            new() { FromId = 1,  ToId = 13, DistanceKm = 0.6 },  // YSU → Republic Square
            new() { FromId = 1,  ToId = 16, DistanceKm = 0.3 },  // YSU → Mashtots Ave

            // AUA connections
            new() { FromId = 2,  ToId = 11, DistanceKm = 0.3 },  // AUA → Metronom
            new() { FromId = 2,  ToId = 14, DistanceKm = 0.3 },  // AUA → Opera
            new() { FromId = 2,  ToId = 17, DistanceKm = 0.4 },  // AUA → Baghramyan

            // NPUA connections
            new() { FromId = 3,  ToId = 13, DistanceKm = 1.1 },  // NPUA → Republic Square
            new() { FromId = 3,  ToId = 15, DistanceKm = 0.5 },  // NPUA → Kyevyan Bridge
            new() { FromId = 3,  ToId = 9,  DistanceKm = 0.8 },  // NPUA → GAA

            // ASUE connections
            new() { FromId = 4,  ToId = 20, DistanceKm = 0.4 },  // ASUE → Komitas
            new() { FromId = 4,  ToId = 19, DistanceKm = 0.5 },  // ASUE → Zoravar

            // YSMU connections
            new() { FromId = 5,  ToId = 11, DistanceKm = 0.7 },  // YSMU → Metronom
            new() { FromId = 5,  ToId = 16, DistanceKm = 0.5 },  // YSMU → Mashtots

            // RAU connections
            new() { FromId = 6,  ToId = 19, DistanceKm = 0.4 },  // RAU → Zoravar
            new() { FromId = 6,  ToId = 20, DistanceKm = 0.6 },  // RAU → Komitas

            // ANAU connections
            new() { FromId = 7,  ToId = 18, DistanceKm = 0.9 },  // ANAU → Tigranakert
            new() { FromId = 7,  ToId = 13, DistanceKm = 1.2 },  // ANAU → Republic Square

            // Brusov connections
            new() { FromId = 8,  ToId = 14, DistanceKm = 0.5 },  // Brusov → Opera
            new() { FromId = 8,  ToId = 17, DistanceKm = 0.3 },  // Brusov → Baghramyan

            // GAA connections
            new() { FromId = 9,  ToId = 16, DistanceKm = 0.4 },  // GAA → Mashtots
            new() { FromId = 9,  ToId = 13, DistanceKm = 0.7 },  // GAA → Republic Square

            // EUAM connections
            new() { FromId = 10, ToId = 17, DistanceKm = 0.6 },  // EUAM → Baghramyan
            new() { FromId = 10, ToId = 20, DistanceKm = 0.8 },  // EUAM → Komitas

            // Landmark connections (connecting the city)
            new() { FromId = 11, ToId = 14, DistanceKm = 0.3 },  // Metronom → Opera
            new() { FromId = 11, ToId = 16, DistanceKm = 0.2 },  // Metronom → Mashtots
            new() { FromId = 12, ToId = 14, DistanceKm = 0.5 },  // Cascade → Opera
            new() { FromId = 12, ToId = 17, DistanceKm = 0.6 },  // Cascade → Baghramyan
            new() { FromId = 13, ToId = 16, DistanceKm = 0.4 },  // Republic Sq → Mashtots
            new() { FromId = 13, ToId = 18, DistanceKm = 0.8 },  // Republic Sq → Tigranakert
            new() { FromId = 14, ToId = 17, DistanceKm = 0.4 },  // Opera → Baghramyan
            new() { FromId = 15, ToId = 12, DistanceKm = 0.9 },  // Kyevyan → Cascade
            new() { FromId = 15, ToId = 13, DistanceKm = 0.7 },  // Kyevyan → Republic Sq
            new() { FromId = 16, ToId = 18, DistanceKm = 0.6 },  // Mashtots → Tigranakert
            new() { FromId = 17, ToId = 19, DistanceKm = 0.5 },  // Baghramyan → Zoravar
            new() { FromId = 19, ToId = 20, DistanceKm = 0.3 },  // Zoravar → Komitas
            new() { FromId = 18, ToId = 15, DistanceKm = 0.5 },  // Tigranakert → Kyevyan
        };

        public List<University> GetAll() => _nodes;

        public List<University> GetUniversitiesOnly() =>
            _nodes.Where(n => n.Id <= 10).ToList();

        public string GetName(int id) =>
            _nodes.FirstOrDefault(u => u.Id == id)?.Name ?? "?";

        public University GetNode(int id) =>
            _nodes.FirstOrDefault(u => u.Id == id);

        public Dictionary<int, List<(int to, double weight)>> GetAdjacencyList()
        {
            var graph = new Dictionary<int, List<(int, double)>>();
            foreach (var u in _nodes)
                graph[u.Id] = new List<(int, double)>();
            foreach (var e in _edges)
            {
                graph[e.FromId].Add((e.ToId, e.DistanceKm));
                graph[e.ToId].Add((e.FromId, e.DistanceKm));
            }
            return graph;
        }
    }
}