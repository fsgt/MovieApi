using System.Collections.Immutable;
using System.Threading.Tasks;
using Demo.Movies.Abstractions.Models;

namespace Demo.Movies.Abstractions.Services
{
    public interface IMovieProvider
    {
        Task<IImmutableList<Movie>> TopRatedAsync();
        Task<IImmutableList<Movie>> UpcomingAsync();
        Task<IImmutableList<Movie>> NowPlayingAsync();
    }
}
