using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordFinder.Application.UseCases.WordFinder.Queries.GetMostRepeatedWordsQuery;
using WordFinder.Application.UseCases.WordFinder.Queries.GetNewMatrixQuery;

namespace WordFinderNet8.WebApi.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class WordFinderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WordFinderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetMostRepeatedWords")]
        public async Task<IActionResult> GetMostRepeatedWords([FromQuery] IEnumerable<string> matrix, [FromQuery] IEnumerable<string> wordStream)
        {
            var response = await _mediator.Send(new GetMostRepeatedWordsQuery()
            {
                matrix = matrix,
                wordStream = wordStream
            });

            if (response.Success)
            {
                return Ok(response); ;
            }
            return BadRequest(response);
        }

        [HttpGet("GetNewMatrix")]
        public async Task<IActionResult> GetNewMatrix([FromQuery] IEnumerable<string> words, [FromQuery] int size)
        {
            var response = await _mediator.Send(new GetNewMatrixQuery()
            {
                words = words,
                size = size
            });

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
