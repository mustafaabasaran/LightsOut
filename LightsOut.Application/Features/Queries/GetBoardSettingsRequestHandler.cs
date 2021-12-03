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
using LightsOut.Domain.Models;
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

        public async Task<BoardSettingDto> Handle(GetBoardSettingsRequest request, CancellationToken cancellationToken)
        {
            var boardSettingsResponseList = await _repository.GetAll();
            
            ValidateBoardSettingsResponse(boardSettingsResponseList);
            
            var response = _mapper.Map<BoardSettingDto>(boardSettingsResponseList[0]);
            return response;
        }

        public void ValidateBoardSettingsResponse(IReadOnlyList<BoardSetting> boardSettingsList)
        {
            if (!boardSettingsList.Any())
                throw new BoardException(ExceptionMessages.EmptyBoardSettingError);

            if (boardSettingsList.Count != 1)
                throw new BoardException(ExceptionMessages.MoreThanOneBoardSettingError);
        }
    }
}