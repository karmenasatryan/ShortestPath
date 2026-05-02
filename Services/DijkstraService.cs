using System;
using System.Collections.Generic;

namespace UniversityGraphAPI.Services
{
    public class DijkstraService
    {
        public (List<int> path, double totalKm) FindShortestPath(
            Dictionary<int, List<(int to, double weight)>> graph,
            int source,
            int target)
        {
           
            if (source == target) return (new List<int> { source }, 0);

            var dist = new Dictionary<int, double>();
            var prev = new Dictionary<int, int>();

            
            var pq = new PriorityQueue<int, double>();

            
            foreach (var node in graph.Keys)
            {
                dist[node] = double.MaxValue;
            }

           
            dist[source] = 0;
            pq.Enqueue(source, 0);

            while (pq.Count > 0)
            {
                int u = pq.Dequeue();

                if (u == target) break;

                if (!graph.ContainsKey(u)) continue;

                foreach (var edge in graph[u])
                {
                    int v = edge.to;
                    double weight = edge.weight;

                    
                    if (!dist.ContainsKey(v)) dist[v] = double.MaxValue;

                    double newDist = dist[u] + weight;
                    if (newDist < dist[v])
                    {
                        dist[v] = newDist;
                        prev[v] = u;
                        pq.Enqueue(v, newDist);
                    }
                }
            }

           
            if (!dist.ContainsKey(target) || dist[target] == double.MaxValue)
                return (new List<int>(), 0);

            
            var path = new List<int>();
            int current = target;

            while (current != source)
            {
                path.Insert(0, current);
                if (!prev.ContainsKey(current)) break;
                current = prev[current];
            }
            path.Insert(0, source);

            return (path, Math.Round(dist[target], 2));
        }
    }
}