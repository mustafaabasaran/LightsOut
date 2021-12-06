using LightsOut.WindowsForm.Model;
using Moq;
using Moq.Contrib.HttpClient;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LightsOut.WindowsForm.UnitTests.Mocks
{
    public static class MockIHttpClientFactory
    {
        private const string baseUri = "http://localhost:85";
        private const string settingUri = "http://localhost:85/v1/BoardSetting";
        private const string listUri = "http://localhost:85/v1/InitialState";
        public static Mock<IHttpClientFactory> GetValidMockIHttpClientFactory()
        {
            var setting = new BoardSetting()
            {
                OffColor = "#FFFF00",
                OnColor = "#00000",
                Size = 5
            };
            var settingsModel = new ServiceResponseModel<BoardSetting>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = setting
            };
            

            var initialStateList = new List<InitialState>()
            {
                new InitialState(){ Row = 0, Column = 0, State = 1},
                new InitialState(){ Row = 0, Column = 2, State = 1},
                new InitialState(){ Row = 1, Column = 4, State = 1},
                new InitialState(){ Row = 2, Column = 4, State = 1},
                new InitialState(){ Row = 3, Column = 1, State = 1},
            };

            var initialStateModel = new ServiceResponseModel<List<InitialState>>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = initialStateList
            };

            var mockFactory = new Mock<IHttpClientFactory>();

            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUri);

            handler.SetupRequest(HttpMethod.Get, settingUri)
                .ReturnsResponse(JsonSerializer.Serialize(settingsModel), "application/json");

            handler.SetupRequest(HttpMethod.Get, listUri)
               .ReturnsResponse(JsonSerializer.Serialize(initialStateModel), "application/json");

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            return mockFactory;
        }

        public static Mock<IHttpClientFactory> GetEmptyMockIHttpClientFactory()
        {
            var setting = new BoardSetting();
            var settingsModel = new ServiceResponseModel<BoardSetting>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = setting
            };

            var initialStateList = new List<InitialState>();

            var initialStateModel = new ServiceResponseModel<List<InitialState>>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = initialStateList
            };

            var mockFactory = new Mock<IHttpClientFactory>();

            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUri);

            handler.SetupRequest(HttpMethod.Get, settingUri)
                .ReturnsResponse(JsonSerializer.Serialize(settingsModel), "application/json");

            handler.SetupRequest(HttpMethod.Get, listUri)
               .ReturnsResponse(JsonSerializer.Serialize(initialStateModel), "application/json");

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            return mockFactory;
        }

        public static Mock<IHttpClientFactory> GetWrongdMockIHttpClientFactory()
        {
            var setting = new BoardSetting()
            {
                OffColor = "#FFFF00",
                OnColor = "#00000",
                Size = 5
            };
            var settingsModel = new ServiceResponseModel<BoardSetting>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = setting
            };

            var initialStateList = new List<InitialState>()
            {
                new InitialState(){ Row = 0, Column = 0, State = 1},
                new InitialState(){ Row = 0, Column = 2, State = 1},
                new InitialState(){ Row = 1, Column = 4, State = 1},
                new InitialState(){ Row = 5, Column = 5, State = 1},
                new InitialState(){ Row = 6, Column = 6, State = 1},
            };

            var initialStateModel = new ServiceResponseModel<List<InitialState>>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = initialStateList
            };

            var mockFactory = new Mock<IHttpClientFactory>();
            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUri);

            handler.SetupRequest(HttpMethod.Get, settingUri)
                .ReturnsResponse(JsonSerializer.Serialize(settingsModel), "application/json");

            handler.SetupRequest(HttpMethod.Get, listUri)
               .ReturnsResponse(JsonSerializer.Serialize(initialStateModel), "application/json");

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            return mockFactory;
        }

        public static Mock<IHttpClientFactory> GetWrongColorMockIHttpClientFactory()
        {
            var setting = new BoardSetting()
            {
                OffColor = "FFFF00",
                OnColor = "00000",
                Size = 5
            };
            var settingsModel = new ServiceResponseModel<BoardSetting>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = setting
            };

            var initialStateList = new List<InitialState>()
            {
                new InitialState(){ Row = 0, Column = 0, State = 1},
                new InitialState(){ Row = 0, Column = 2, State = 1},
                new InitialState(){ Row = 1, Column = 4, State = 1},
            };

            var initialStateModel = new ServiceResponseModel<List<InitialState>>()
            {
                Header = new ServiceResponseHeader()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success"
                },
                Data = initialStateList
            };

            var mockFactory = new Mock<IHttpClientFactory>();
            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUri);

            handler.SetupRequest(HttpMethod.Get, settingUri)
                .ReturnsResponse(JsonSerializer.Serialize(settingsModel), "application/json");

            handler.SetupRequest(HttpMethod.Get, listUri)
               .ReturnsResponse(JsonSerializer.Serialize(initialStateModel), "application/json");

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            return mockFactory;
        }

    }
}
