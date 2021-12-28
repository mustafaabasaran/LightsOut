using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace LightsOut.Api.IntegrationTests.Helpers
{
    public class DistributedCacheMock : IDistributedCache
    {
        public byte[] Get(string key)
        {
            return Encoding.UTF8.GetBytes(String.Empty);
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = new CancellationToken())
        {
            return Task.FromResult(Encoding.UTF8.GetBytes(String.Empty));
        }

        public void Refresh(string key)
        {
            
        }

        public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            
        }

        public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
            CancellationToken token = new CancellationToken())
        {
            return Task.CompletedTask;
        }
    }
}