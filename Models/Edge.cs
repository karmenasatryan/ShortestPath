namespace UniversityGraphAPI.Models
{
    public class Edge
    {
        public int Id { get; set; } // Սա պարտադիր պետք է լինի
        public int FromId { get; set; }
        public int ToId { get; set; }
        public double DistanceKm { get; set; }
    }
}