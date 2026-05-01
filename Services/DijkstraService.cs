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
            // 1. Եթե սկզբնակետն ու վերջնակետը նույնն են
            if (source == target) return (new List<int> { source }, 0);

            var dist = new Dictionary<int, double>();
            var prev = new Dictionary<int, int>();

            // .NET 6+ PriorityQueue-ն պահանջում է երկու տիպ (Element, Priority)
            var pq = new PriorityQueue<int, double>();

            // 2. Նախնական հեռավորությունների սահմանում
            foreach (var node in graph.Keys)
            {
                dist[node] = double.MaxValue;
            }

            // Համոզվենք, որ source-ը կա dist բառարանում, նույնիսկ եթե այն graph.Keys-ում չէ
            dist[source] = 0;
            pq.Enqueue(source, 0);

            while (pq.Count > 0)
            {
                int u = pq.Dequeue();

                // 3. Օպտիմիզացիա. եթե հասանք նպատակակետին, դադարեցնում ենք
                if (u == target) break;

                if (!graph.ContainsKey(u)) continue;

                foreach (var edge in graph[u])
                {
                    int v = edge.to;
                    double weight = edge.weight;

                    // Համոզվենք, որ հարևան հանգույցը գոյություն ունի dist բառարանում
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

            // 4. Ստուգում՝ արդյոք ճանապարհ գտնվել է
            if (!dist.ContainsKey(target) || dist[target] == double.MaxValue)
                return (new List<int>(), 0);

            // 5. Երթուղու վերականգնում
            var path = new List<int>();
            int current = target;

            while (current != source)
            {
                path.Insert(0, current);
                if (!prev.ContainsKey(current)) break;
                current = prev[current];
            }
            path.Insert(0, source);

            return (path, Math.Round(dist[target], 2)); // Կլորացում 2 նիշի (Frontend-ի համար ավելի հարմար է)
        }
    }
}