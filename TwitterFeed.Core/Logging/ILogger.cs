
using System;

namespace TwitterFeed.Core.Logging
{
    public interface ILogger
    {
        void Info(string message, object source = null);

        void Error(Exception ex, object source = null);

        void Error(string message, object source = null);

        void Event(string message, Exception exception, object source = null);
    }
}
