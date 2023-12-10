using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cint.CodingChallenge.Model.DTO.Responses;
using Microsoft.AspNetCore.WebUtilities;
using Shouldly;
using Xunit;

namespace Cint.CodingChallenge.Web.Test
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SurveySearch()
        {
            var query = new Dictionary<string, string>
            {
                ["Name"] = "Survey",
                ["Number"] = "10",
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("/survey/search", query!));

            response.ShouldNotBeNull();

            var objectResponse = response.ShouldBeOfType<HttpResponseMessage>();
            objectResponse.IsSuccessStatusCode.ShouldBeTrue();

            await using var contentStream =  await response.Content.ReadAsStreamAsync();            
            var surveyList = await JsonSerializer.DeserializeAsync<List<SurveyViewModel>>(contentStream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            
            surveyList.ShouldNotBeNull();
            surveyList.ShouldBeOfType<List<SurveyViewModel>>();
            surveyList.Count.ShouldBe(5);
            
            for (var i = 0; i < surveyList.Count - 1; i++)
            {
                var currentItemEfficiency = (double)(surveyList[i].IncentiveEuros / surveyList[i].LengthMinutes)!;
                var nextItemEfficiency = (double)(surveyList[i+1].IncentiveEuros / surveyList[i+1].LengthMinutes)!;
            
                currentItemEfficiency.ShouldBeGreaterThanOrEqualTo(nextItemEfficiency);
            }
        }

    }
}
