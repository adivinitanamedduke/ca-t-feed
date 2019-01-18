using System;

namespace TwitterFeed.Core.Utils
{

    public static class Guard
    {
        public static void NotNull(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
        }

        public static void NotNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
