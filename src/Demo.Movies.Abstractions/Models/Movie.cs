using System;

namespace Demo.Movies.Abstractions.Models
{
    [Serializable]
    public class Movie
    {
        public string Title { get; init; }
        public string OriginalTitle { get; init; }
        public string Overview { get; init; }
        public double VoteAverage { get; init; }
        public int VoteCount { get; init; }
        public double Popularity { get; init; }
    }
}
