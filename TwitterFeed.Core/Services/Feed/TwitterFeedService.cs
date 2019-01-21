using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterFeed.Core.Entities;
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
		public void CreateFeed(List<User> users, List<Tweet> tweets)
		{
			users.ForEach(u => {
				_printService.printUser(u);
				tweets.ForEach(t => {
					if (t.User == u)
					{
						_printService.printTweet(t);
					} else if (t.User.Followers.Any(f => f.Name == u.Name))
					{
						_printService.printTweet(t);
					}
				});
			});
		}

	}
}
