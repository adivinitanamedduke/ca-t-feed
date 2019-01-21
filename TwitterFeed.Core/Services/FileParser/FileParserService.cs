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

		public List<User> ParseUser(StreamReader streamReader)
		{
			try
			{
				using (StreamReader file = streamReader)
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
								var user = userList.Single(u => u.Name == usersInLine[i].Trim());
								user.Followers.Add(userList.Single(f => f.Name == usersInLine[0].Trim())); 
							}
						}
						else
						{
							_logger.Error("Invalid line, no users in line.");
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

		public List<Tweet> ParseTweet(StreamReader streamReader, List<User> users)
		{
			try
			{
				using (StreamReader file = streamReader)
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
						} else
						{
							_logger.Error("Invalid tweet, has invalid number of entries");
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
