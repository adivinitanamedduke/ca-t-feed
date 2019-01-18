using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterFeed.Core.Entities;
using TwitterFeed.Core.Enums;
using TwitterFeed.Core.Services.Printing;

namespace TwitterFeed.Core.Services.Feed
{
	public class TwitterFeedService : ITwitterFeedService
	{
		private readonly ITwitterPrintService _printService;
		public TwitterFeedService(ITwitterPrintService printService)
		{
			_printService = printService;
		}
		public void CreateFeed(List<User> users, List<Tweet> tweets, PrintOrder printOrder)
		{
			switch (printOrder)
			{
				case PrintOrder.DESC:
					users.OrderByDescending(u => u.Name);
					break;
				case PrintOrder.ASC:
				default:
					users.OrderBy(u => u.Name);
					break;
			}

			//TODO: print tweets in order of occurence and by specified order
					
		}
		public void CreateFeed(List<User> users, List<Tweet> tweets)
		{
			CreateFeed(users, tweets, PrintOrder.ASC);
		}
	}
}
