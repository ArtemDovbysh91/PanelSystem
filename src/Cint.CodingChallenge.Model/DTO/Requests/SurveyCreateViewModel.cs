namespace Cint.CodingChallenge.Model.DTO.Requests;

public class SurveyCreateViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public int LengthMinutes { get; set; }
    public double IncentiveEuros { get; set; }
}
