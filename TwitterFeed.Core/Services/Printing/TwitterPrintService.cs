using System;
using System.Collections.Generic;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Services.Printing
{
	public class TwitterPrintService : ITwitterPrintService
	{
		public void printTweetsByUser(User user, List<Tweet> tweets)
		{
			Console.WriteLine(user.Name);
			if (tweets.Count > 0)
				tweets.ForEach(t => Console.WriteLine(String.Format("\t @{0}: {1}", t.User, t.Message)));

		}
	}
}
