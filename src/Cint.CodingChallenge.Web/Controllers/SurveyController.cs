using Cint.CoddingChallenge.Business;
using Cint.CodingChallenge.Model.DTO.Requests;
using Cint.CodingChallenge.Model.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Cint.CodingChallenge.Web.Controllers;

[ApiController]
[Route("survey")]
public class SurveyController : Controller
{
    private readonly ISurveyService _surveyService;

    public SurveyController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] SearchQueryViewModel searchModel)
    {
        var result = await _surveyService.Search(searchModel);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var survey = await _surveyService.GetSurveyById(id);
            
        return Ok(survey);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SurveyCreateViewModel surveyCreateViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        SurveyViewModel result;
        
        try
        {
            result = await _surveyService.Create(surveyCreateViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
            
        return Ok();
    }
}