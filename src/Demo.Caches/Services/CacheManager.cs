using System;
using System.Threading.Tasks;
using Demo.Caches.Abstractions.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Demo.Caches.Services
{
    internal class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptionsMonitor<CacheProfileOptions> _optionsMonitor;

        public CacheManager(IMemoryCache memoryCache, IOptionsMonitor<CacheProfileOptions> optionsMonitor)
        {
            _memoryCache = memoryCache;
            _optionsMonitor = optionsMonitor;
        }

        public async Task<T> GetOrAddAsync<T>(string section, string intrasectionalId, Func<Task<T>> factory)
        {
            var options = _optionsMonitor.Get(section);
            if (options == null || options.InMemoryAbsoluteExpiration <= TimeSpan.Zero)
            {
                return await factory();
            }

            var key = string.Join('|', section, intrasectionalId);
            var entry = await _memoryCache.GetOrCreateAsync(key, entry => 
            {
                entry.AbsoluteExpirationRelativeToNow = options.InMemoryAbsoluteExpiration;
                return factory();
            });

            return entry;
        }
    }
}
