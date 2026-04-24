namespace UniversityGraphAPI.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }  // Ճշգրիտ դիրքի համար
        public double Longitude { get; set; } // Ճշգրիտ դիրքի համար
    }
}