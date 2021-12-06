using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LightsOut.Api.Model;
using LightsOut.Application.DTOs;
using Shouldly;
using Xunit;

namespace LightsOut.Api.IntegrationTests
{
    public class InitialStateControllerTests : IntegrationTests
    {
        [Fact]
        public async Task Get_BoardSetting_Successful()
        {
            var response = await TestClient.GetAsync("v1/InitialState");
            
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var responseObject = await response.Content.ReadFromJsonAsync<ServiceResponseModel<List<InitialStateDto>>>();
            responseObject.ShouldNotBeNull();
            responseObject.Header.Message.ShouldBe("SUCCESS");
            responseObject.Header.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            responseObject.Data.Count.ShouldBe(5);
        }
    }
}