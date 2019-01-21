using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterFeed.Core.Concepts
{
	public static class Delimeters
	{
		public const string UserLineDelimiter = "follows";
		public const string TweetLineDelimiter = "> ";
		public const string FolloweeListDelimiter = ",";
	}
}
