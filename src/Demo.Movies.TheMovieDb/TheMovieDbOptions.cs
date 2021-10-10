namespace Demo.Movies.TheMovieDb
{
    internal class TheMovieDbOptions
    {
        public const string Section = "TheMovieDatabase";

        public string ApiKeyV3 { get; init; }
        public string ApiKeyV4 { get; init; }
    }
}
