namespace Cint.CodingChallenge.Model.DBSet
{
    public class Survey
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LengthMinutes { get; set; } = 0;
        public double IncentiveEuros { get; set; } = 0.0;
    }
}

