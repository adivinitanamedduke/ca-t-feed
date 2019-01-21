using System;
using System.Collections.Generic;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Services.Printing
{
	public class TwitterPrintService : ITwitterPrintService
	{
		public void printTweet(Tweet tweet)
		{
			Console.WriteLine(String.Format("\t @{0}: {1}", tweet.User.Name, tweet.Message));
		}
		public void printUser(User user)
		{
			Console.WriteLine(user.Name);
		}
	}
}
