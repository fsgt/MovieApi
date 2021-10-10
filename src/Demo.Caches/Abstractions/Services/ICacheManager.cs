using System;
using System.Threading.Tasks;

namespace Demo.Caches.Abstractions.Services
{
    internal interface ICacheManager
    {
        Task<T> GetOrAddAsync<T>(string section, string intrasectionalId, Func<Task<T>> factory);
    }
}
