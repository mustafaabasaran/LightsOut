using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LightsOut.Application.DTOs;
using LightsOut.Application.Features.Requests;
using LightsOut.Application.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LightsOut.Application.Features.Queries
{
    public class GetBoardSettingsRequestHandler : IRequestHandler<GetBoardSettingsRequest, BoardSettingDto>
    {
        private readonly ILogger<GetBoardSettingsRequestHandler> _logger;
        private readonly IBoardSettingRepository _repository;
        private readonly IMapper _mapper;

        public GetBoardSettingsRequestHandler(IBoardSettingRepository repository, IMapper mapper, ILogger<GetBoardSettingsRequestHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<BoardSettingDto> Handle(GetBoardSettingsRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}