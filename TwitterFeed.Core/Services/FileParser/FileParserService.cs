using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TwitterFeed.Core.Concepts;
using TwitterFeed.Core.Entities;
using TwitterFeed.Core.Factories;
using TwitterFeed.Core.Logging;
using TwitterFeed.Core.Utils;

namespace TwitterFeed.Core.Services.FileParser
{
	public class FileParserService : IFileParserService
    {
		private readonly ILogger _logger;
		public FileParserService (ILogger logger)
		{
			_logger = logger;
		}

		public List<User> ParseUser(string filePath)
		{
			try
			{
				using (StreamReader file = new System.IO.StreamReader(filePath))
				{
					string line;

					SortedSet<User> userList = new SortedSet<User>();
					while ((line = file.ReadLine()) != null)
					{
						string[] usersInLine = line.Split(new string[] { Delimeters.UserLineDelimiter, Delimeters.FolloweeListDelimiter }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()).ToArray();
						if (usersInLine.Length > 0)
						{
							foreach (var u in usersInLine)
							{
								//Create a user entry
								var usr = UserFactory.CreateUser(u);
								userList.Add(usr);
							}
							//Set up follower list
							for (int i = usersInLine.Length - 1; i > 0; i--)
							{
								var user = userList.Single(u => (u as User).Name == usersInLine[i].Trim());
								(user as User).Followers.Add(userList.Single(f => (f as User).Name == usersInLine[0].Trim()) as User); 
							}
						}
					}
					return userList.ToList();
				}
			}
			catch (IOException ex)
			{
				_logger.Error(ex);
			}
			return null;
		}

		public List<Tweet> ParseTweet(string filePath, List<User> users)
		{
			try
			{
				using (StreamReader file = new System.IO.StreamReader(filePath))
				{
					//TODO: File processing
					string line;

					List<Tweet> tweets = new List<Tweet>();
					while ((line = file.ReadLine()) != null)
					{
						string[] tweetLine = line.Split(new string[] { Delimeters.TweetLineDelimiter }, StringSplitOptions.RemoveEmptyEntries);
						if (tweetLine.Length > 0 && tweetLine.Length == 2)
						{
							var user = users.Find(u => u.Name == tweetLine[0].Trim());
							if(user != null)
							{
								tweets.Add(TweetFactory.CreateTweet(user, tweetLine[1]));
							}
						}
					}
					return tweets;
				}
			}
			catch (IOException ex)
			{
				_logger.Error(ex);
			}
			return null;
		}
	}
}
