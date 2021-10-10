using System.Collections.Immutable;
using Demo.Movies.Abstractions.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Demo.Movies.TheMovieDb.Abstractions.Services
{
    internal interface IMovieMapper
    {
        IImmutableList<Movie> Map(SearchContainer<SearchMovie> source);
    }
}