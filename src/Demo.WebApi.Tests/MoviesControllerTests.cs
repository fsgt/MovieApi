using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Demo.Movies.Abstractions.Models;
using Demo.Movies.Abstractions.Services;
using Demo.WebApi.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Demo.WebApi.Tests
{
    public class MoviesControllerTests
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<IMovieProvider> _movieProvider;

        public MoviesControllerTests()
        {
            _movieProvider = new Mock<IMovieProvider>();
            var services = new ServiceCollection();
            services.AddTransient(_ => _movieProvider.Object);
            services.AddTransient<MoviesController>();
            _provider = services.BuildServiceProvider();
        }

        private static Func<Task<IImmutableList<Movie>>> CreateMovieResponse(Movie[] movies)
            => () => Task.FromResult<IImmutableList<Movie>>(ImmutableList.Create(movies));

        private Movie CreateMovie(string title)
            => new Movie
            {
                Title = title
            };

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("movie1")]
        [InlineData("movie1", null)]
        [InlineData("movie1", "movie2")]
        public async Task NowPlaying_Ok(params string[] movies)
        {
            var expectedResult = movies?.Select(CreateMovie).ToArray()
                ?? Array.Empty<Movie>();

            _movieProvider.Setup(p => p.NowPlayingAsync())
                .Returns(CreateMovieResponse(expectedResult));

            var controller = _provider.GetRequiredService<MoviesController>();
            var actionResult = await controller.NowPlayingAsync();

            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);

            var output = Assert.IsAssignableFrom<IEnumerable<Movie>>(result.Value);
            Assert.Equal(expectedResult, output);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("movie1")]
        [InlineData("movie1", null)]
        [InlineData("movie1", "movie2")]
        public async Task TopRated_Ok(params string[] movies)
        {
            var expectedResult = movies?.Select(CreateMovie).ToArray()
                ?? Array.Empty<Movie>();

            _movieProvider.Setup(p => p.TopRatedAsync())
                .Returns(CreateMovieResponse(expectedResult));

            var controller = _provider.GetRequiredService<MoviesController>();
            var actionResult = await controller.TopRatedAsync();

            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);

            var output = Assert.IsAssignableFrom<IEnumerable<Movie>>(result.Value);
            Assert.Equal(expectedResult, output);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("movie1")]
        [InlineData("movie1", null)]
        [InlineData("movie1", "movie2")]
        public async Task Upcoming_Ok(params string[] movies)
        {
            var expectedResult = movies?.Select(CreateMovie).ToArray()
                ?? Array.Empty<Movie>();

            _movieProvider.Setup(p => p.UpcomingAsync())
                .Returns(CreateMovieResponse(expectedResult));

            var controller = _provider.GetRequiredService<MoviesController>();
            var actionResult = await controller.UpcomingAsync();

            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);

            var output = Assert.IsAssignableFrom<IEnumerable<Movie>>(result.Value);
            Assert.Equal(expectedResult, output);
        }
    }
}
