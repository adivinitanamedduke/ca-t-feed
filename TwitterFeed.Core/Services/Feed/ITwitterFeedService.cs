using System;
using System.Collections.Generic;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Services.Feed
{
	public interface ITwitterFeedService
	{
		void CreateFeed(List<User> users, List<Tweet> tweets);
	}
}
