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
using Newtonsoft.Json;

namespace LightsOut.Application.Features.Queries
{
    public class GetInitialStateListRequestHandler : IRequestHandler<GetInitialStateListRequest, List<InitialStateDto>>
    {
        private readonly ILogger<GetInitialStateListRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IInitialStateRepository _initialStateRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly string cacheKey;
        
        public GetInitialStateListRequestHandler(ILogger<GetInitialStateListRequestHandler> logger, IMapper mapper, IInitialStateRepository initialStateRepository, IDistributedCache distributedCache)
        {
            _logger = logger;
            _mapper = mapper;
            _initialStateRepository = initialStateRepository;
            _distributedCache = distributedCache;
            cacheKey = "GetAllInitialStates";
        }

        public async Task<List<InitialStateDto>> Handle(GetInitialStateListRequest request, CancellationToken cancellationToken)
        {
            IReadOnlyList<InitialState> initialStateList;
            var initialStateListCacheResponse = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(initialStateListCacheResponse))
            {
                initialStateList = await _initialStateRepository.GetAll();
                string initialStateListCachedValue = JsonConvert.SerializeObject(initialStateList);
                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                await _distributedCache.SetStringAsync(cacheKey, initialStateListCachedValue, option, token: cancellationToken);
            }
            else
            {
                initialStateList =
                    JsonConvert.DeserializeObject<IReadOnlyList<InitialState>>(initialStateListCacheResponse);
            }
            
            if (!initialStateList.Any())
                throw new BoardException(ExceptionMessages.EmptyInitialStateError);

            var response = _mapper.Map<List<InitialStateDto>>(initialStateList);
            return response;
        }
    }
}