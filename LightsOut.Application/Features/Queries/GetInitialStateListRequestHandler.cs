using System.Collections.Generic;
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
    public class GetInitialStateListRequestHandler : IRequestHandler<GetInitialStateListRequest, List<InitialStateDto>>
    {
        private readonly ILogger<GetInitialStateListRequestHandler> _logger;
        private readonly IBoardSettingRepository _boardSettingRepository;
        private readonly IMapper _mapper;
        private readonly IInitialStateRepository _initialStateRepository;

        public GetInitialStateListRequestHandler(ILogger<GetInitialStateListRequestHandler> logger, IBoardSettingRepository boardSettingRepository, IMapper mapper, IInitialStateRepository initialStateRepository)
        {
            _logger = logger;
            _boardSettingRepository = boardSettingRepository;
            _mapper = mapper;
            _initialStateRepository = initialStateRepository;
        }

        public Task<List<InitialStateDto>> Handle(GetInitialStateListRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}