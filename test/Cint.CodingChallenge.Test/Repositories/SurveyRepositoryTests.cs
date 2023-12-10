using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cint.CodingChallenge.Data;
using Cint.CodingChallenge.Data.Repositories;
using Cint.CodingChallenge.Model.DBSet;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace Cint.CodingChallenge.Web.Test.Repositories;

public class SurveyRepositoryTests
{
    private static readonly Guid FirstSurveyId = Guid.NewGuid();
        
    private IQueryable<Survey>  _surveys = new List<Survey>()
    {
        new Survey
        {
            Id = FirstSurveyId,
            Name = "Test one name",
            Description = "Test one description",
            IncentiveEuros = 1.2,
            LengthMinutes = 5
        },
        new Survey
        {
            Id = Guid.NewGuid(),
            Name = "Test two name",
            Description = "Test two description",
            IncentiveEuros = 1.1,
            LengthMinutes = 4
        }
    }.AsQueryable();

    private readonly SurveyRepository _surveyRepository;

    public SurveyRepositoryTests()
    {
        var mockSet = new Mock<DbSet<Survey>>();
            
        mockSet.As<IQueryable<Survey>>().Setup(m => m.Provider).Returns(new AsyncHelper.TestAsyncQueryProvider<Survey>(_surveys.Provider));
        mockSet.As<IQueryable<Survey>>().Setup(m => m.Expression).Returns(_surveys.Expression);
        mockSet.As<IQueryable<Survey>>().Setup(m => m.ElementType).Returns(_surveys.ElementType);
        mockSet.As<IAsyncEnumerable<Survey>>()
            .Setup(m => m.GetAsyncEnumerator(default))
            .Returns(new AsyncHelper.TestAsyncEnumerator<Survey>(_surveys.GetEnumerator()));

        mockSet.Setup(m => m.AddAsync(It.IsAny<Survey>(),
                It.IsAny<CancellationToken>()))
            .Callback<Survey, CancellationToken>((a, _) =>
            {
                _surveys.ToList().Add(a);
            })
            .Returns<Survey, CancellationToken>((a, _) => AccountEntityEntryHelper.CreateEntityEntry(a));

        var mockDataContext = new Mock<DatabaseContext>();
        mockDataContext.Setup(c => c.Surveys).Returns(mockSet.Object);

        _surveyRepository = new SurveyRepository(mockDataContext.Object);
    }

    [Fact]
    public async Task Search_surveys_returns()
    {
        // Arrange
        var search = new SearchModel { Name = string.Empty, Number = 10 };
            
        // Act
        var result = await _surveyRepository.Search(search);
        var resultArray = result.ToArray();
        
        // Assert
        resultArray.ShouldNotBeNull();
        resultArray.Length.ShouldBe(2);
    }
        
    [Fact]
    public async Task Search_surveys_returns_one()
    {
        // Arrange
        var search = new SearchModel { Name = string.Empty, Number = 1 };
            
        // Act
        var result = await _surveyRepository.Search(search);
        var resultArray = result.ToArray();

        // Assert
        resultArray.ShouldNotBeNull();
        resultArray.Length.ShouldBe(1);
    }
        
    [Fact]
    public async Task Search_surveys_filter_one_returns_one()
    {
        // Arrange
        var search = new SearchModel { Name = "one", Number = 1 };
            
        // Act
        var result = await _surveyRepository.Search(search);
        var resultArray = result.ToArray();
        
        // Assert
        resultArray.ShouldNotBeNull();
        resultArray.Length.ShouldBe(1);
    }
        
    [Fact]
    public async Task Search_surveys_upper_case_returns_empty_collection()
    {
        // Arrange
        var search = new SearchModel { Name = "ONE", Number = 1 };
            
        // Act
        var result = await _surveyRepository.Search(search);
        var resultArray = result.ToArray();

        // Assert
        resultArray.ShouldNotBeNull();
        resultArray.Length.ShouldBe(0);
    }
        
    [Fact]
    public async Task Search_surveys_lower_case_returns_two_surveys()
    {
        // Arrange
        var search = new SearchModel { Name = "test", Number = 10 };
            
        // Act
        var result = await _surveyRepository.Search(search);
        var resultArray = result.ToArray();

        // Assert
        resultArray.ShouldNotBeNull();
        resultArray.Length.ShouldBe(2);
    }

    [Fact]
    public async Task Get_all_surveys()
    {
        // Arrange
            
        // Act
        var result = await _surveyRepository.GetSurveys();
        var resultArray = result.ToArray();

        // Assert
        resultArray.ShouldNotBeNull();
        resultArray.Length.ShouldBe(_surveys.Count());
    }
        
    [Fact]
    public async Task Get_survey_by_id()
    {
        // Arrange
            
        // Act
        var result = await _surveyRepository.GetSurveyById(FirstSurveyId);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldNotBeEmpty();
    }
        
    [Fact]
    public async Task Insert_survey_test()
    {
        // Arrange
        var survey = new Survey()
        {
            Name = "test name",
            Description = "test description",
            IncentiveEuros = 123.45,
            LengthMinutes = 6,
        };
            
        // Act
        var result = await _surveyRepository.InsertSurvey(survey);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldNotBeEmpty();
    }
    
    [Fact]
    public async Task Search_order__test()
    {
        // Arrange
            
        // Act
        var result = await _surveyRepository.GetSurveys();
        var resultArray = result.ToArray();
        
        // Assert
        resultArray.ShouldNotBeNull();
        for (var i = 0; i < resultArray.Length - 1; i++)
        {
            var currentItemEfficiency = resultArray[i].IncentiveEuros / resultArray[i].LengthMinutes;
            var nextItemEfficiency = resultArray[i+1].IncentiveEuros / resultArray[i+1].LengthMinutes;
            
            currentItemEfficiency.ShouldBeLessThanOrEqualTo(nextItemEfficiency);
        }
    }
}