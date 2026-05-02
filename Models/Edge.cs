namespace UniversityGraphAPI.Models
{
    public class Edge
    {
        public int Id { get; set; } 
        public int FromId { get; set; }
        public int ToId { get; set; }
        public double DistanceKm { get; set; }
    }
}