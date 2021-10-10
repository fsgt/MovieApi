using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Demo.Movies.Abstractions.Models;
using Demo.Movies.Abstractions.Services;
using Demo.Movies.TheMovieDb.Abstractions.Services;
using Demo.Movies.TheMovieDb.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xunit;

namespace Demo.Movies.TheMovieDb.Tests
{
    public class MovieProviderTests
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<ITheMovieDbClient> _theMovieDbClient;
        private readonly Mock<IMovieMapper> _movieMapper;

        public MovieProviderTests()
        {
            _theMovieDbClient = new Mock<ITheMovieDbClient>();
            _movieMapper = new Mock<IMovieMapper>();

            _movieMapper.Setup(p => p.Map(It.IsAny<SearchContainer<SearchMovie>>()))
                .Returns<SearchContainer<SearchMovie>>(a => a
                    .Results?
                    .Where(movie => movie is not null)
                    .Select(movie => new Movie { Title = movie.Title })
                    .ToImmutableList()
                );

            var services = new ServiceCollection();
            services.AddTransient(_ => _theMovieDbClient.Object);
            services.AddTransient(_ => _movieMapper.Object);
            services.AddTransient<IMovieProvider, MovieProvider>();
            _provider = services.BuildServiceProvider();
        }

        private static Func<Task<SearchContainer<SearchMovie>>> CreateSearchContainer(string[] movies)
            => () => Task.FromResult(new SearchContainer<SearchMovie> { Results = movies?.Select(m => m != null ? new SearchMovie { Title = m } : null).ToList() });

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("movie1")]
        [InlineData("movie1", null)]
        [InlineData("movie1", "movie2")]
        public async Task NowPlaying_Ok(params string[] movies)
        {
            _theMovieDbClient.Setup(p => p.NowPlayingMoviesAsync())
                .Returns(CreateSearchContainer(movies));
            
            var movieProvider = _provider.GetRequiredService<IMovieProvider>();
            var result = await movieProvider.NowPlayingAsync();

            Assert.Equal(movies?.Where(m => m != null), result?.Select(movie => movie.Title));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("movie1")]
        [InlineData("movie1", null)]
        [InlineData("movie1", "movie2")]
        public async Task TopRated_Ok(params string[] movies)
        {
            _theMovieDbClient.Setup(p => p.TopRatedMoviesAsync())
                .Returns(CreateSearchContainer(movies));

            var movieProvider = _provider.GetRequiredService<IMovieProvider>();
            var result = await movieProvider.TopRatedAsync();

            Assert.Equal(movies?.Where(m => m != null), result?.Select(movie => movie.Title));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("movie1")]
        [InlineData("movie1", null)]
        [InlineData("movie1", "movie2")]
        public async Task Upcoming_Ok(params string[] movies)
        {
            _theMovieDbClient.Setup(p => p.UpcomingMoviesAsync())
                .Returns(CreateSearchContainer(movies));

            var movieProvider = _provider.GetRequiredService<IMovieProvider>();
            var result = await movieProvider.UpcomingAsync();

            Assert.Equal(movies?.Where(m => m != null), result?.Select(movie => movie.Title));
        }
    }
}
