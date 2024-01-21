using Cint.CodingChallenge.Model.DTO.Requests;
using Cint.CodingChallenge.Model.DTO.Responses;

namespace Cint.CodingChallenge.Business;

public interface ISurveyService
{
    Task<List<SurveyViewModel>> Search(SearchQueryViewModel search);
    Task<SurveyViewModel?> GetSurveyById(Guid id);
    Task<SurveyViewModel> Create(SurveyCreateViewModel surveyCreateViewModel);
}