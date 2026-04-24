namespace UniversityGraphAPI.Services
{
    public class DijkstraService
    {
        public (List<int> path, double totalKm) FindShortestPath(
            Dictionary<int, List<(int to, double weight)>> graph,
            int source,
            int target)
        {
            // Սկզբում բոլոր հեռավորությունները անսահման են
            var dist = new Dictionary<int, double>();
            var prev = new Dictionary<int, int>();
            var pq = new PriorityQueue<int, double>();

            foreach (var node in graph.Keys)
                dist[node] = double.MaxValue;

            // Մեկնման կետի հեռավորությունը 0 է
            dist[source] = 0;
            pq.Enqueue(source, 0);

            while (pq.Count > 0)
            {
                int u = pq.Dequeue();

                if (u == target) break;

                foreach (var (v, weight) in graph[u])
                {
                    double newDist = dist[u] + weight;

                    if (newDist < dist[v])
                    {
                        dist[v] = newDist;
                        prev[v] = u;
                        pq.Enqueue(v, newDist);
                    }
                }
            }

            // Ճանապարհը վերականգնել
            var path = new List<int>();
            for (int at = target; prev.ContainsKey(at); at = prev[at])
                path.Insert(0, at);
            path.Insert(0, source);

            return (path, Math.Round(dist[target], 2));
        }
    }
}
