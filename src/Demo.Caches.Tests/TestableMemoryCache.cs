using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Demo.Caches.Tests
{
    internal class TestableMemoryCache : IMemoryCache
    {
        private readonly ConcurrentDictionary<object, CacheEntry> _cache = new ConcurrentDictionary<object, CacheEntry>();

        public ICacheEntry GetEntry(object key)
            => _cache[key];

        private long _totalRemoved = 0;
        private long _totalCreated = 0;
        private long _totalRead = 0;
        private long _totalReadFailed = 0;

        public long TotalValues => _cache.Count;
        public long TotalRemoved => Interlocked.Read(ref _totalRemoved);
        public long TotalCreated => Interlocked.Read(ref _totalCreated);
        public long TotalRead => Interlocked.Read(ref _totalRead);
        public long TotalReadFailed => Interlocked.Read(ref _totalReadFailed);

        public ICacheEntry CreateEntry(object key)
        {
            var entry = new CacheEntry
            {
                Key = key,
            };

            Interlocked.Increment(ref _totalCreated);
            _cache.TryAdd(key, entry);

            return entry;
        }

        public void Remove(object key)
        {
            Interlocked.Increment(ref _totalRemoved);
            _cache.Remove(key, out _);
        }

        public bool TryGetValue(object key, out object value)
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                Interlocked.Increment(ref _totalRead);
                value = entry.Value;
                return true;
            }

            Interlocked.Increment(ref _totalReadFailed);
            value = default;
            return false;
        }

        public void Dispose()
        {

        }

        internal class CacheEntry : ICacheEntry
        {
            public object Key { get; init; }

            public object Value { get; set; }
            public DateTimeOffset? AbsoluteExpiration { get; set; }
            public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
            public TimeSpan? SlidingExpiration { get; set; }

            public IList<IChangeToken> ExpirationTokens { get; } = new List<IChangeToken>();

            public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; } = new List<PostEvictionCallbackRegistration>();

            public CacheItemPriority Priority { get; set; }
            public long? Size { get; set; }

            public void Dispose()
            {

            }
        }
    }
}
