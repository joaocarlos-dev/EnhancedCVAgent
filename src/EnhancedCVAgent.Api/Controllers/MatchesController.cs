using EnhancedCVAgent.Application.Matching.Commands;
using EnhancedCVAgent.Application.Matching.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnhancedCVAgent.Api.Controllers
{
    [ApiController]
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly ISender _sender;

        public MatchesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MatchResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MatchResultDto>> CalculateMatch(
            [FromBody] CalculateCandidateJobMatchCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}

