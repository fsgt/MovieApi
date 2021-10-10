using System.Collections.Immutable;
using System.Linq;
using Demo.Movies.Abstractions.Models;
using Demo.Movies.TheMovieDb.Abstractions.Services;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Demo.Movies.TheMovieDb.Services
{
    internal class MovieMapper : IMovieMapper
    {
        public virtual IImmutableList<Movie> Map(SearchContainer<SearchMovie> source)
        {
            var builder = ImmutableList.CreateBuilder<Movie>();
            foreach (var item in source?.Results ?? Enumerable.Empty<SearchMovie>())
            {
                if (item is null)
                    continue;

                builder.Add(MapItem(item));
            }

            return builder.ToImmutable();
        }

        private Movie MapItem(SearchMovie movie)
        {
            return new Movie
            {
                Title = movie.Title,
                OriginalTitle = movie.OriginalTitle,
                Overview = movie.Overview,
                VoteAverage = movie.VoteAverage,
                VoteCount = movie.VoteCount,
                Popularity = movie.Popularity,
            };
        }
    }
}
