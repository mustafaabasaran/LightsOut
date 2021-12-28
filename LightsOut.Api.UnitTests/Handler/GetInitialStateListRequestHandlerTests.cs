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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace LightsOut.Api.UnitTests.Handler
{
    public class GetInitialStateListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IInitialStateRepository> _mockRepo;
        private ILogger<GetInitialStateListRequestHandler> _logger;
        private readonly IDistributedCache _distributedCache;

        public GetInitialStateListRequestHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<GetInitialStateListRequestHandler>>().Object;
            _distributedCache = new Mock<IDistributedCache>().Object;
        }

        [Fact]
        public async Task Should_Work()
        {
            var mockRepo = MockInitialStateRepository.GetInitialStateRepository();
            var handler = new GetInitialStateListRequestHandler(_logger, _mapper, mockRepo.Object,_distributedCache);
            var request = new GetInitialStateListRequest();

            var response = await handler.Handle(request, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Count.ShouldBe(3);
        }
        
        [Fact]
        public async Task Should_EmptyList_Throw_Exception()
        {
            var mockRepo = MockInitialStateRepository.GetInitialStateRepositoryWithEmptyList();
            var handler = new GetInitialStateListRequestHandler(_logger, _mapper, mockRepo.Object,_distributedCache);
            var request = new GetInitialStateListRequest();

            var ex = await Should.ThrowAsync<BoardException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
           
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<BoardException>();
            ex.Message.ShouldBe(ExceptionMessages.EmptyInitialStateError);
        }
    }
}