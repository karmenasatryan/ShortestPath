using Microsoft.AspNetCore.Mvc;
using UniversityGraphAPI.Models;
using UniversityGraphAPI.Services;

namespace UniversityGraphAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PathController : ControllerBase
    {
        private readonly GraphService _graph;
        private readonly DijkstraService _dijkstra;

        public PathController(GraphService graph, DijkstraService dijkstra)
        {
            _graph = graph;
            _dijkstra = dijkstra;
        }

        [HttpGet("universities")]
        public IActionResult GetUniversities()
        {
            return Ok(_graph.GetUniversitiesOnly());
        }

        [HttpPost("shortest-path")]
        public IActionResult GetShortestPath([FromBody] PathRequest req)
        {
            var graph = _graph.GetAdjacencyList();
            var result = _dijkstra.FindShortestPath(graph, req.FromId, req.ToId);

            var namedPath = result.path.Select(id => new {
                id,
                name = _graph.GetName(id),
                node = _graph.GetNode(id)
            });

            return Ok(new
            {
                path = namedPath,
                totalKm = result.totalKm
            });
        }
    }
}