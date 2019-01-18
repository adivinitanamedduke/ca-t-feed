using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using TwitterFeed.Core.Entities;
using TwitterFeed.Core.Logging;
using TwitterFeed.Core.Services.Feed;
using TwitterFeed.Core.Services.FileParser;
using TwitterFeed.Core.Services.Printing;
using TwitterFeed.Core.Utils;

namespace TwitterFeed
{
	public class Program
	{
		public static void Main(string[] args)
		{
			List<User> users = new List<User>();
			List<Tweet> tweets = new List<Tweet>();

			//Setup Dependency Injection
			var serviceProvider = new ServiceCollection()
			.AddScoped<ILogger, ConsoleLogger>()
			.AddScoped<IFileParserService, FileParserService>()
			.AddScoped<ITwitterPrintService, TwitterPrintService>()
			.AddScoped<ITwitterFeedService, TwitterFeedService>()
			.BuildServiceProvider();

			var logger = serviceProvider.GetRequiredService<ILogger>();
			if (args.Length == 2)
			{
				var userFile = args[0];
				var tweetFile = args[1];
				Guard.NotNullOrEmpty(userFile);
				Guard.NotNullOrEmpty(tweetFile);

				var processFileService = serviceProvider.GetRequiredService<IFileParserService>();
				processFileService.Parse(userFile).ForEach(u => users.Add(u as User));
				processFileService.Parse(tweetFile).ForEach(t => tweets.Add(t as Tweet));

				var feed = serviceProvider.GetService<ITwitterFeedService>();
				feed.CreateFeed(users, tweets);

			}
			else
			{
				logger.Error("Invalid number of arguments, Press Enter and try again.");
				Console.Read();
			}
		}
	}
}
