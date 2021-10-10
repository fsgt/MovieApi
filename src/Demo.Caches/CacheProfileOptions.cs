using System;

namespace Demo.Caches
{
    internal class CacheProfileOptions
    {
        public const string OptionPrefix = "Caches";

        public TimeSpan InMemoryAbsoluteExpiration { get; init; }
    }
}
