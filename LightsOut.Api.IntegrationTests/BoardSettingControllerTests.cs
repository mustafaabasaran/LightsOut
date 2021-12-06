using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LightsOut.Api.Model;
using LightsOut.Application.DTOs;
using Shouldly;
using Xunit;

namespace LightsOut.Api.IntegrationTests
{
    public class BoardSettingControllerTests : IntegrationTests
    {
        
        [Fact]
        public async Task Get_BoardSetting_Successful()
        {
            var response = await TestClient.GetAsync("v1/BoardSetting");
            
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var responseObject = await response.Content.ReadFromJsonAsync<ServiceResponseModel<BoardSettingDto>>();
            responseObject.ShouldNotBeNull();
            responseObject.Header.Message.ShouldBe("SUCCESS");
            responseObject.Data.Size.ShouldBe(5);
            responseObject.Data.OffColor.ShouldBe("#000000");
            responseObject.Data.OnColor.ShouldBe("#0300ff");
        }
    }
}