using System.Collections.Generic;
using LightsOut.Application.Persistence;
using LightsOut.Domain.Models;
using Moq;

namespace LightsOut.Api.UnitTests.Mocks
{
    public class MockInitialStateRepository
    {
        public static Mock<IInitialStateRepository> GetInitialStateRepositoryWithEmptyList()
        {
            var initialStateList = new List<InitialState>();
            var mockRepo = new Mock<IInitialStateRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(initialStateList);
            return mockRepo;
        }
        
        public static Mock<IInitialStateRepository> GetInitialStateRepository()
        {
            var initialStateList = new List<InitialState>()
            {
                new InitialState()
                {
                    Id = 1,
                    Column = 0,
                    Row = 1,
                    State = 1
                },
                new InitialState()
                {
                    Id = 2,
                    Column = 3,
                    Row = 2,
                    State = 1
                },
                new InitialState()
                {
                    Id = 3,
                    Column = 0,
                    Row = 3,
                    State = 1
                },
            };
            var mockRepo = new Mock<IInitialStateRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(initialStateList);
            return mockRepo;
        }
    }
}