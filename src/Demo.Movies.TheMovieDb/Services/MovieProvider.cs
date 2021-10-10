using System.Collections.Immutable;
using System.Threading.Tasks;
using Demo.Movies.Abstractions.Models;
using Demo.Movies.Abstractions.Services;
using Demo.Movies.TheMovieDb.Abstractions.Services;

namespace Demo.Movies.TheMovieDb.Services
{
    internal class MovieProvider : IMovieProvider
    {
        private readonly IMovieMapper _movieMapper;
        private readonly ITheMovieDbClient _theMovieDbClient;

        public MovieProvider(IMovieMapper movieMapper, ITheMovieDbClient theMovieDbClient)
        {
            _movieMapper = movieMapper;
            _theMovieDbClient = theMovieDbClient;
        }

        public async Task<IImmutableList<Movie>> NowPlayingAsync()
        {
            var result = await _theMovieDbClient.NowPlayingMoviesAsync();
            return _movieMapper.Map(result);
        }

        public async Task<IImmutableList<Movie>> TopRatedAsync()
        {
            var result = await _theMovieDbClient.TopRatedMoviesAsync();
            return _movieMapper.Map(result);
        }

        public async Task<IImmutableList<Movie>> UpcomingAsync()
        {
            var result = await _theMovieDbClient.UpcomingMoviesAsync();
            return _movieMapper.Map(result);
        }
    }
}
