using System;
using System.Collections.Generic;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Services.Printing
{
	public interface ITwitterPrintService
	{
		void printTweet(Tweet tweet);
		void printUser(User user);
	}
}
