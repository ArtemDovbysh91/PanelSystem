using Cint.CodingChallenge.Model.DBSet;

namespace Cint.CodingChallenge.Data.Repositories;

public interface ISurveyRepository : IDisposable
{
    Task<IEnumerable<Survey>> Search(SearchModel search);
    Task<IEnumerable<Survey>> GetSurveys();
    Task<Survey?> GetSurveyById(Guid surveyId);
    Task<Survey> InsertSurvey(Survey survey);
    Task Save();
}