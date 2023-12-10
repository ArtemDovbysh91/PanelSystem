using Cint.CodingChallenge.Model.DBSet;

namespace Cint.CodingChallenge.Model.DTO.Responses;

public class SurveyViewModel
{
    public SurveyViewModel()
    {
        
    }
    
    public SurveyViewModel(Survey? s)
    {
        Id = s?.Id;
        Name = s?.Name;
        Description = s?.Description;
        LengthMinutes = s?.LengthMinutes;
        IncentiveEuros = s?.IncentiveEuros;
    }
    
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? LengthMinutes { get; set; }
    public double? IncentiveEuros { get; set; }
}