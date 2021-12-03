using System.Collections.Generic;
using LightsOut.Application.Persistence;
using LightsOut.Domain.Models;
using Moq;

namespace LightsOut.Api.UnitTests.Mocks
{
    public static class MockBoardSettingsRepository
    {
        public static Mock<IBoardSettingRepository> GetLeaveRepositoryWithEmptyList()
        {
            var boardSettingList = new List<BoardSetting>();
            var mockRepo = new Mock<IBoardSettingRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(boardSettingList);
            return mockRepo;
        }
        
        public static Mock<IBoardSettingRepository> GetLeaveRepositoryWithOneRecord()
        {
            var boardSettingList = new List<BoardSetting>()
            {
                new BoardSetting()
                {
                    Id = 1,
                    OffColor = $"#000000",
                    OnColor = $"#FFF000",
                    Size = 5
                }
            };

            var mockRepo = new Mock<IBoardSettingRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(boardSettingList);
            return mockRepo;
        }

        public static Mock<IBoardSettingRepository> GetLeaveRepositoryWithMoreThanOneRecord()
        {
            var boardSettingList = new List<BoardSetting>()
            {
                new BoardSetting()
                {
                    Id = 1,
                    OffColor = $"#000000",
                    OnColor = $"#FFF000",
                    Size = 5
                },
                new BoardSetting()
                {
                    Id = 2,
                    OffColor = $"#000000",
                    OnColor = $"#FFF000",
                    Size = 6
                }
            };

            var mockRepo = new Mock<IBoardSettingRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(boardSettingList);
            return mockRepo;
        }

    }
}