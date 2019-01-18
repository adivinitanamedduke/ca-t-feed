
using TwitterFeed.Core.Logging;
using TwitterFeed.Core.Utils;
using System;
using System.Collections.Generic;

namespace TwitterFeed.Core.Caching
{
    public sealed class InMemoryCacheProvider : ICacheProvider
    {
        private readonly ILogger _logger;

        public InMemoryCacheProvider(ILogger logger)
        {
            Guard.NotNull(logger);

            this._logger = logger;

            logger.Info("Created", this);
        }

        private static readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        private static string CreateInternalCacheKey(Type type, object key)
        {
            return string.Format("cachekey_{0}_{1}", type, key);
        }

        public T Get<T>(object key, Func<T> getObjectFunc)
        {
            var cachableObj = Get<T>(key);

            if (cachableObj == null)
            {
                cachableObj = getObjectFunc();

                if (cachableObj != null)
                {
                    Save<T>(cachableObj, key);
                }
            }

            return cachableObj;
        }

        public T Get<T>(object key)
        {
            var internalKey = CreateInternalCacheKey(typeof(T), key);

            if (_cache.ContainsKey(internalKey))
            {
                _logger.Info(string.Format("Cache hit for key '{0}'", internalKey), this);

                return (T)_cache[internalKey];
            }

            _logger.Info(string.Format("Cache miss for key '{0}'", internalKey), this);

            return default(T);
        }

        public bool Exists<T>(object key)
        {
            var internalKey = CreateInternalCacheKey(typeof(T), key);

            return _cache.ContainsKey(internalKey);
        }

        public bool Remove<T>(object key)
        {
            if (key == null) return false;

            var internalKey = CreateInternalCacheKey(typeof(T), key);

            return _cache.Remove(internalKey);
        }

        public void Save<T>(T obj, object key)
        {
            var internalKey = CreateInternalCacheKey(typeof(T), key);

            _cache[internalKey] = obj;
        }
    }
}
