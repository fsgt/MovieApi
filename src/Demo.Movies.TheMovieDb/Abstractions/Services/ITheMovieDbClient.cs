using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Demo.Movies.TheMovieDb.Abstractions.Services
{
    internal interface ITheMovieDbClient
    {
        Task<SearchContainer<SearchMovie>> NowPlayingMoviesAsync();
        Task<SearchContainer<SearchMovie>> TopRatedMoviesAsync();
        Task<SearchContainer<SearchMovie>> UpcomingMoviesAsync();
    }
}