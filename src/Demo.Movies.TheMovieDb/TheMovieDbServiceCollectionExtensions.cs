using Demo.Movies.Abstractions.Services;
using Demo.Movies.TheMovieDb;
using Demo.Movies.TheMovieDb.Abstractions.Services;
using Demo.Movies.TheMovieDb.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TheMovieDbServiceCollectionExtensions
    {
        public static IServiceCollection AddTheMovieDbMovies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TheMovieDbOptions>(configuration.GetSection(TheMovieDbOptions.Section));
            services.AddSingleton<ITheMovieDbClient, TheMovieDbClient>();
            services.AddSingleton<IMovieMapper, MovieMapper>();
            services.AddSingleton<IMovieProvider, MovieProvider>();

            return services;
        }
    }
}
