using Microsoft.AspNetCore.Mvc;
using UniversityGraphAPI.Models;
using UniversityGraphAPI.Services;
using System.Linq;

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

        [HttpPost("shortest-path")]
        public IActionResult GetShortestPath([FromBody] PathRequest req)
        {
            // 1. Ստանում ենք գրաֆը և կատարում Dijkstra հաշվարկը
            var graph = _graph.GetAdjacencyList();
            var result = _dijkstra.FindShortestPath(graph, req.FromId, req.ToId);

            if (result.path == null || !result.path.Any())
            {
                return NotFound("Ճանապարհ չի գտնվել:");
            }

            // 2. Ձևավորում ենք պատասխանը Frontend-ի համար (ներառյալ կոորդինատները)
            var formattedPath = result.path.Select(id => {
                var node = _graph.GetNode(id); // Ստանում ենք ամբողջ Node օբյեկտը
                return new
                {
                    id = id,
                    name = _graph.GetName(id),
                    lat = node.Latitude, // Համոզվեք, որ ձեր Node-ի մեջ Lat/Lng են գրված
                    lng = node.Longitude
                };
            });

            return Ok(new
            {
                path = formattedPath,
                totalKm = System.Math.Round(result.totalKm, 2) // Կլորացում 2 նիշով
            });
        }

        [HttpGet("universities")]
        public IActionResult GetUniversities()
        {
            return Ok(_graph.GetUniversitiesOnly());
        }
    }
}