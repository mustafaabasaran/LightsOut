using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LightsOut.Api.UnitTests.Mocks;
using LightsOut.Application.Exceptions;
using LightsOut.Application.Features.Queries;
using LightsOut.Application.Features.Requests;
using LightsOut.Application.Persistence;
using LightsOut.Application.Profiles;
using LightsOut.Application.Resources;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace LightsOut.Api.UnitTests.Handler
{
    public class GetBoardSettingsRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBoardSettingRepository> _mockRepo;
        private ILogger<GetBoardSettingsRequestHandler> _logger;

        public GetBoardSettingsRequestHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<GetBoardSettingsRequestHandler>>().Object;
        }
        [Fact]
        public async Task Should_Work()
        {
            var _mockRepo = MockBoardSettingsRepository.GetLeaveRepositoryWithOneRecord();
            var handler = new GetBoardSettingsRequestHandler(_mockRepo.Object, _mapper, _logger);
            var request = new GetBoardSettingsRequest();
            
            var response = await handler.Handle(request, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Size.ShouldBe(5);
        }
        
        [Fact]
        public async Task Should_Throw_Empty_Settings_Exception()
        {
            var _mockRepo = MockBoardSettingsRepository.GetLeaveRepositoryWithEmptyList();
            var handler = new GetBoardSettingsRequestHandler(_mockRepo.Object, _mapper, _logger);
            var request = new GetBoardSettingsRequest();
            
            var ex = await Should.ThrowAsync<BoardException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });

            ex.ShouldBeOfType<BoardException>();
            ex.ShouldNotBeNull();
            ex.Message.ShouldBe(ExceptionMessages.EmptyBoardSettingError);
        }
        
        [Fact]
        public async Task Should_Throw_MoreThanOneRecord_Settings_Exception()
        {
            var _mockRepo = MockBoardSettingsRepository.GetLeaveRepositoryWithMoreThanOneRecord();
            var handler = new GetBoardSettingsRequestHandler(_mockRepo.Object, _mapper, _logger);
            var request = new GetBoardSettingsRequest();
            
            var ex = await Should.ThrowAsync<BoardException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });

            ex.ShouldBeOfType<BoardException>();
            ex.ShouldNotBeNull();
            ex.Message.ShouldBe(ExceptionMessages.MoreThanOneBoardSettingError);
        }
    }
}