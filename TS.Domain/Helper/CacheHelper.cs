using Microsoft.Extensions.Caching.Distributed;
using System;

namespace TS.Domain.Helper
{
	public class CacheHelper
	{
        private readonly IDistributedCache _cache;

        public CacheHelper(IDistributedCache database)
        {
            _cache = database;
        }

        public string Get(string key)
        {
            return this._cache.GetString(key);
        }

        public void Set(string key, string value, TimeSpan? expireTimeSpan = null)
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();

            if (expireTimeSpan != null)
            {
                cacheOptions.SetAbsoluteExpiration(expireTimeSpan.Value);
            }

            this._cache.SetString(key, value, cacheOptions);
        }

        public void Remove(string key)
        {
            this._cache.Remove(key);
        }
    }
}
