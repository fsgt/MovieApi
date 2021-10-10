using System.Threading.Tasks;
using Demo.Movies.TheMovieDb.Abstractions.Services;
using Microsoft.Extensions.Options;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Demo.Movies.TheMovieDb.Services
{
    internal class TheMovieDbClient : ITheMovieDbClient
    {
        private readonly IOptionsMonitor<TheMovieDbOptions> _options;

        public TheMovieDbClient(IOptionsMonitor<TheMovieDbOptions> options)
        {
            _options = options;
        }

        public virtual async Task<SearchContainer<SearchMovie>> NowPlayingMoviesAsync()
        {
            using var client = new TMDbClient(_options.CurrentValue.ApiKeyV3);
            return await client.GetMovieNowPlayingListAsync();
        }

        public virtual async Task<SearchContainer<SearchMovie>> TopRatedMoviesAsync()
        {
            using var client = new TMDbClient(_options.CurrentValue.ApiKeyV3);
            return await client.GetMovieTopRatedListAsync();
        }

        public virtual async Task<SearchContainer<SearchMovie>> UpcomingMoviesAsync()
        {
            using var client = new TMDbClient(_options.CurrentValue.ApiKeyV3);
            return await client.GetMovieUpcomingListAsync();
        }
    }
}
