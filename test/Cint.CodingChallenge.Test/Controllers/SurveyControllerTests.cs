using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Threading.Tasks;
using Cint.CoddingChallenge.Business;
using Cint.CodingChallenge.Model.DTO.Requests;
using Cint.CodingChallenge.Model.DTO.Responses;
using Cint.CodingChallenge.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace Cint.CodingChallenge.Web.Test.Controllers;

public class SurveyControllerTests
{
    private readonly SurveyController _surveyController;

    private readonly List<SurveyViewModel> _searchResult = new()
    {
        new SurveyViewModel()
        {
            Id = Guid.NewGuid(),
            Name = "Test one name",
            Description = "Test one description",
            IncentiveEuros = 1.2,
            LengthMinutes = 5
        },
        new SurveyViewModel()
        {
            Id = Guid.NewGuid(),
            Name = "Test two name",
            Description = "Test two description",
            IncentiveEuros = 1.1,
            LengthMinutes = 4
        }
    };
    
    public SurveyControllerTests()
    {
        var mockService = new Mock<ISurveyService>();

        mockService.Setup(s => s.Search(It.IsAny<SearchQueryViewModel>()))
            .Returns((SearchQueryViewModel q) => Task.FromResult(_searchResult.Where(s => s.Name.Contains(q.Name)).ToList()));
        
        _surveyController = new SurveyController(mockService.Object);
    }

    [Fact]
    public async Task Search_surveys_returns()
    {
        var search = new SearchQueryViewModel { Name = string.Empty, Number = 10 };
            
        var response = await _surveyController.Search(search);

        response.ShouldNotBeNull();
        
        var objectResponse = response.ShouldBeOfType<OkObjectResult>();
        objectResponse.StatusCode.ShouldBe(200);
        
        objectResponse.Value.ShouldNotBeNull();
        objectResponse.Value.ShouldBeOfType<List<SurveyViewModel>>();
        var actual = (response as OkObjectResult)?.Value as List<SurveyViewModel>;
        actual.ShouldNotBeNull();
        actual.Count.ShouldBe(2);
    }
        
    [Fact]
    public async Task Search_filters_surveys_by_substring()
    {
        var response = await _surveyController.Search(new SearchQueryViewModel { Name = "two" });

        response.ShouldNotBeNull();

        var objectResponse = response.ShouldBeOfType<OkObjectResult>();
        objectResponse.StatusCode.ShouldBe(200);

        objectResponse.Value.ShouldNotBeNull();
        objectResponse.Value.ShouldBeOfType<List<SurveyViewModel>>();
        var actual = (response as OkObjectResult)?.Value as List<SurveyViewModel>;
        actual.ShouldNotBeNull();
        actual.Count.ShouldBe(1);
    }
}