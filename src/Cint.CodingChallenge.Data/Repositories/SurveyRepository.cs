using Cint.CodingChallenge.Model.DBSet;
using Microsoft.EntityFrameworkCore;

namespace Cint.CodingChallenge.Data.Repositories;

public class SurveyRepository : ISurveyRepository, IDisposable
{
    private readonly DatabaseContext _context;
    private readonly DbSet<Survey> _dbSet;
    
    public SurveyRepository(DatabaseContext context)
    {
        _context = context;
        _dbSet = context.Surveys;
    }

    public async Task<IEnumerable<Survey>> Search(SearchModel search)
    {
        return await _dbSet.Where(s => search.Name != null && s.Name.ToLowerInvariant().Contains(search.Name))
            .OrderByDescending(s => s.IncentiveEuros/s.LengthMinutes)
            .Take(search.Number)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Survey>> GetSurveys()
    {
        return await _context.Surveys.ToListAsync();
    }

    public Task<Survey?> GetSurveyById(Guid surveyId)
    {
        return _dbSet.FirstOrDefaultAsync(s => s.Id == surveyId);
    }

    public async Task<Survey> InsertSurvey(Survey survey)
    {
        var result = await _dbSet.AddAsync(survey);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
    
    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}