using System;
using System.Collections.Generic;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Factories
{
	public static class TweetFactory
	{
		public static Tweet CreateTweet(User user, string message)
		{
			return new Tweet(user, message.Trim());
		}
	}
}
