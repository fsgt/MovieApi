using System;
using System.Threading.Tasks;
using Demo.Caches.Abstractions.Services;
using Demo.Caches.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Demo.Caches.Tests
{
    public class CacheManagerTests
    {
        private readonly IServiceProvider _provider;
        private readonly TestableMemoryCache _memoryCache;

        public CacheManagerTests()
        {
            var enabledOption = new CacheProfileOptions
            {
                InMemoryAbsoluteExpiration = TimeSpan.FromDays(3650),
            };

            var disabledOption = new CacheProfileOptions
            {
                InMemoryAbsoluteExpiration = TimeSpan.Zero,
            };

            var options = new Mock<IOptionsMonitor<CacheProfileOptions>>();

            options.Setup(a => a.Get("enabled"))
                .Returns(enabledOption);
            options.Setup(a => a.Get("disabled"))
                .Returns(disabledOption);

            _memoryCache = new TestableMemoryCache();
            var services = new ServiceCollection();
            services.AddTransient<ICacheManager, CacheManager>();
            services.AddTransient(_ => options.Object);
            services.AddTransient<IMemoryCache>(_ => _memoryCache);

            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task DisableCache()
        {
            var cache = _provider.GetRequiredService<ICacheManager>();
            var result = await cache.GetOrAddAsync("disabled", "1", () => Task.FromResult("result"));
            Assert.Equal("result", result);

            result = await cache.GetOrAddAsync("disabled", "1", () => Task.FromResult("other result"));
            Assert.Equal("other result", result);

            result = await cache.GetOrAddAsync("disabled", "1", () => Task.FromResult("other result 2"));
            Assert.Equal("other result 2", result);

            Assert.Equal(0, _memoryCache.TotalCreated);
            Assert.Equal(0, _memoryCache.TotalRead);
        }

        [Fact]
        public async Task EnableCache()
        {
            var cache = _provider.GetRequiredService<ICacheManager>();

            var result = await cache.GetOrAddAsync("enabled", "1", () => Task.FromResult("result"));
            Assert.Equal(1, _memoryCache.TotalCreated);
            Assert.Equal("result", result);

            result = await cache.GetOrAddAsync("enabled", "1", () => Task.FromResult("other result"));
            Assert.Equal(1, _memoryCache.TotalCreated);
            Assert.Equal("result", result);

            result = await cache.GetOrAddAsync("enabled", "2", () => Task.FromResult("other result 2"));
            Assert.Equal(2, _memoryCache.TotalCreated);
            Assert.Equal("other result 2", result);

            result = await cache.GetOrAddAsync("enabled", "1", () => Task.FromResult("other result 3"));
            Assert.Equal(2, _memoryCache.TotalCreated);
            Assert.Equal("result", result);
        }
    }
}
