using System.Collections.Generic;
using LightsOut.Application.DTOs;
using MediatR;

namespace LightsOut.Application.Features.Requests
{
    public class GetInitialStateListRequest : IRequest<List<InitialStateDto>>
    {
        
    }
}