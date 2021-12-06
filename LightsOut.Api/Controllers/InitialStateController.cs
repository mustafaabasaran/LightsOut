using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LightsOut.Api.Model;
using LightsOut.Application.DTOs;
using LightsOut.Application.Features.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LightsOut.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class InitialStateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InitialStateController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<InitialStateDto>>> GetInitialStateList()
        {
            var request = new GetInitialStateListRequest();
            var responseList = await _mediator.Send(request);
            var response = new ServiceResponseModel<List<InitialStateDto>>()
            {
                Data = responseList,
                Header = new ServiceResponseHeader()
            };
            return Ok(response);
        }
    }
}