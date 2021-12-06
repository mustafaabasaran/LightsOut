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
    public class BoardSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoardSettingController(IMediator mediator)
        {
            _mediator =  mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<BoardSettingDto>> GetSettings()
        {
            var request = new GetBoardSettingsRequest();
            var settingsDtoResponse = await _mediator.Send(request);
            var response = new ServiceResponseModel<BoardSettingDto>()
            {
                Data = settingsDtoResponse,
                Header = new ServiceResponseHeader()
            };
            return Ok(response);
        }
    }
}