using Cint.CodingChallenge.Data.Repositories;
using Cint.CodingChallenge.Model.DBSet;
using Cint.CodingChallenge.Model.DTO.Requests;
using Cint.CodingChallenge.Model.DTO.Responses;

namespace Cint.CodingChallenge.Business;

public class SurveyService: ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;

    public SurveyService(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }
    
    public async Task<List<SurveyViewModel>> Search(SearchQueryViewModel search)
    {
        var surveys = await _surveyRepository
            .Search(new SearchModel()
            {
                Name = search.Name.Trim().ToLowerInvariant(),
                Number = search.Number ?? 1000
            });
 
        return surveys.Select(s => new SurveyViewModel(s)).ToList();
    }

    public async Task<SurveyViewModel?> GetSurveyById(Guid id)
    {
        var survey = await _surveyRepository.GetSurveyById(id);

        return new SurveyViewModel(survey);
    }
    
    public async Task<SurveyViewModel> Create(SurveyCreateViewModel surveyCreateViewModel)
    {
        var result = await _surveyRepository.InsertSurvey(new Survey()
        {
            Name = surveyCreateViewModel.Name,
            Description = surveyCreateViewModel.Description ?? string.Empty,
            IncentiveEuros = surveyCreateViewModel.IncentiveEuros,
            LengthMinutes = surveyCreateViewModel.LengthMinutes
        });

        return new SurveyViewModel(result);
    }
}