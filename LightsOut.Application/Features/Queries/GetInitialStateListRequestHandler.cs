using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LightsOut.Application.DTOs;
using LightsOut.Application.Exceptions;
using LightsOut.Application.Features.Requests;
using LightsOut.Application.Persistence;
using LightsOut.Application.Resources;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LightsOut.Application.Features.Queries
{
    public class GetInitialStateListRequestHandler : IRequestHandler<GetInitialStateListRequest, List<InitialStateDto>>
    {
        private readonly ILogger<GetInitialStateListRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IInitialStateRepository _initialStateRepository;

        public GetInitialStateListRequestHandler(ILogger<GetInitialStateListRequestHandler> logger, IMapper mapper, IInitialStateRepository initialStateRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _initialStateRepository = initialStateRepository;
        }

        public async Task<List<InitialStateDto>> Handle(GetInitialStateListRequest request, CancellationToken cancellationToken)
        {
            var initialStateList = await _initialStateRepository.GetAll();
            if (!initialStateList.Any())
                throw new BoardException(ExceptionMessages.EmptyInitialStateError);

            var response = _mapper.Map<List<InitialStateDto>>(initialStateList);
            return response;
        }
    }
}