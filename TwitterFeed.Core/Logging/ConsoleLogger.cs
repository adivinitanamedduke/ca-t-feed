using TwitterFeed.Core.Logging;
using System;

namespace TwitterFeed.Core.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Error(Exception ex, object source = null)
        {
            Console.WriteLine(ex.ToString());
        }

        public void Error(string message, object source = null)
        {
            Console.WriteLine(message);
        }

        public void Event(string message, Exception exception, object source = null)
        {
            Console.WriteLine(string.Format("{0} - {1}", message, exception.ToString()));
        }

        public void Info(string message, object source = null)
        {
            Console.WriteLine(message);
        }
    }
}
