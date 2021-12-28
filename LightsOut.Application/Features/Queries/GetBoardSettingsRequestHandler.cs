using System;
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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using Newtonsoft.Json;

namespace LightsOut.Application.Features.Queries
{
    public class GetBoardSettingsRequestHandler : IRequestHandler<GetBoardSettingsRequest, BoardSettingDto>
    {
        private readonly ILogger<GetBoardSettingsRequestHandler> _logger;
        private readonly IBoardSettingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly string cacheKey;

        public GetBoardSettingsRequestHandler(IBoardSettingRepository repository, IMapper mapper, ILogger<GetBoardSettingsRequestHandler> logger, IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _distributedCache = distributedCache;
            cacheKey = "GetAllBoardSettings";
        }

        public async Task<BoardSettingDto> Handle(GetBoardSettingsRequest request, CancellationToken cancellationToken)
        {
            IReadOnlyList<BoardSetting> boardSettingsResponseList;
            var boardSettingsCacheResponseList = _distributedCache.GetString(cacheKey);
            if (string.IsNullOrEmpty(boardSettingsCacheResponseList))
            {
                boardSettingsResponseList = await _repository.GetAll();
                string boardSettingsCachedValue = JsonConvert.SerializeObject(boardSettingsResponseList);
                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                await _distributedCache.SetStringAsync(cacheKey, boardSettingsCachedValue, option, token: cancellationToken);
            }
            else
            {
                boardSettingsResponseList =
                    JsonConvert.DeserializeObject<IReadOnlyList<BoardSetting>>(boardSettingsCacheResponseList);
            }

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