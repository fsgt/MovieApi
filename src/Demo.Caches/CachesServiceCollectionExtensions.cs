using Demo.Caches;
using Demo.Caches.Abstractions.Services;
using Demo.Caches.Decorators;
using Demo.Caches.Services;
using Demo.Movies.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CachesServiceCollectionExtensions
    {
        public static IServiceCollection AddCaches(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheProfileOptions>(CacheSections.Movies, configuration.GetSection(string.Join(":", CacheProfileOptions.OptionPrefix, CacheSections.Movies)));
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, CacheManager>();

            // Scrutor is included to access the decorate method, this could be included from some other framework or manually implemented.
            services.Decorate<IMovieProvider, MovieProviderCache>();

            return services;
        }
    }
}
