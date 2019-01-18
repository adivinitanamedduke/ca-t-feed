using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterFeed.Core.Caching
{
    public interface ICacheProvider 
    {
        T Get<T>(object key);
        bool Exists<T>(object key);

        T Get<T>(object key, Func<T> getObjectFunc);

        void Save<T>(T obj, object key);

        bool Remove<T>(object key);
    }
}
