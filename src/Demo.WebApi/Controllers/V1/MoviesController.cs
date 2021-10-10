using System.Collections.Immutable;
using System.Threading.Tasks;
using Demo.Movies.Abstractions.Models;
using Demo.Movies.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.Controllers.V1
{
    [ApiController]
    [Route("v{version:apiVersion}/movies")]
    [ApiVersion("1")]
    public class MoviesController
    {
        private readonly IMovieProvider _movieProvider;

        public MoviesController(IMovieProvider movieProvider)
        {
            _movieProvider = movieProvider;
        }

        [HttpGet("now-playing")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IImmutableList<Movie>))]
        public async Task<IActionResult> NowPlayingAsync()
        {
            var result = await _movieProvider.NowPlayingAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("top-rated")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IImmutableList<Movie>))]
        public async Task<IActionResult> TopRatedAsync()
        {
            var result = await _movieProvider.TopRatedAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("upcoming")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IImmutableList<Movie>))]
        public async Task<IActionResult> UpcomingAsync()
        {
            var result = await _movieProvider.UpcomingAsync();
            return new OkObjectResult(result);
        }
    }
}
