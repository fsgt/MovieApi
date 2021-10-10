using System.Collections.Immutable;
using System.Threading.Tasks;
using Demo.Caches.Abstractions.Services;
using Demo.Movies.Abstractions.Models;
using Demo.Movies.Abstractions.Services;

namespace Demo.Caches.Decorators
{
    internal class MovieProviderCache : IMovieProvider
    {
        private readonly IMovieProvider _movieProvider;
        private readonly ICacheManager _cacheManager;

        public MovieProviderCache(IMovieProvider movieProvider, ICacheManager cacheManager)
        {
            _movieProvider = movieProvider;
            _cacheManager = cacheManager;
        }

        public async Task<IImmutableList<Movie>> NowPlayingAsync()
            => await _cacheManager.GetOrAddAsync(
                CacheSections.Movies,
                "now-playing",
                () => _movieProvider.NowPlayingAsync()
            );

        public async Task<IImmutableList<Movie>> TopRatedAsync()
            => await _cacheManager.GetOrAddAsync(
                CacheSections.Movies,
                "top-rated",
                () => _movieProvider.TopRatedAsync()
            );

        public async Task<IImmutableList<Movie>> UpcomingAsync()
            => await _cacheManager.GetOrAddAsync(
                CacheSections.Movies,
                "upcoming",
                () => _movieProvider.UpcomingAsync()
            );
    }
}
