using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TwitterFeed.Core.Entities;
using TwitterFeed.Core.Logging;
using TwitterFeed.Core.Services.Feed;
using TwitterFeed.Core.Services.FileParser;
using TwitterFeed.Core.Services.Printing;

namespace TwitterFeed.Core.Tests
{
	[TestClass]
	public class ServicesTests
	{
		ITwitterFeedService _twitterFeedService;
		ILogger _logger;
		IFileParserService _fileParserService;
		ITwitterPrintService _twitterPrintService;
		[TestInitialize]
		public void TestSetup()
		{
			_logger = A.Fake<ILogger>();
			_twitterFeedService = A.Fake<ITwitterFeedService>();
			_fileParserService = A.Fake<FileParserService>();
			_twitterPrintService = A.Fake<ITwitterPrintService>();

		}

		[TestMethod]
		public void Make_Sure_The_ParserService_Can_Parse_Correct_User_Line()
		{
			Assembly thisAssembly = Assembly.GetExecutingAssembly();
			string path = "TwitterFeed.Core.Tests";
			var userList = _fileParserService.ParseUser(new StreamReader(thisAssembly.GetManifestResourceStream(path + "." +"userTestData.txt")));
			Assert.IsTrue(userList[0].Name == "Linda");
		}

		[TestMethod]
		public void Make_Sure_The_ParserService_Can_Parse_Correct_Tweet_Line()
		{
			Assembly thisAssembly = Assembly.GetExecutingAssembly();
			string path = "TwitterFeed.Core.Tests";
			var userList = _fileParserService.ParseUser(new StreamReader(thisAssembly.GetManifestResourceStream(path + "." + "userTestData.txt")));
			var tweets = _fileParserService.ParseTweet(new StreamReader(thisAssembly.GetManifestResourceStream(path + "." + "tweetTestData.txt")), userList);
			Assert.IsTrue(userList.Count == 8);
			Assert.IsTrue(tweets[0].User.Name == "Zach");
			Assert.IsTrue(tweets[0].Message == "If you have a procedure with 10 parameters, you probably missed some.");

		}

		[TestMethod]
		public void Make_Sure_The_TwitterFeedService_Can_Successfully_CreateFeed()
		{
			List<User> users = new List<User>();
			users.Add(Factories.UserFactory.CreateUser("Alan"));
			users.Add(Factories.UserFactory.CreateUser("Martin"));
			users.Add(Factories.UserFactory.CreateUser("Ward"));

			List<Tweet> tweets = new List<Tweet>();
			tweets.Add(Factories.TweetFactory.CreateTweet(users.Find(u => u.Name == "Alan"), "If you have a procedure with 10 parameters, you probably missed some"));
			tweets.Add(Factories.TweetFactory.CreateTweet(users.Find(u => u.Name == "Alan"), "Random numbers should not be generated with a method chosen at random? You copied it from somewhere."));
			tweets.Add(Factories.TweetFactory.CreateTweet(users.Find(u => u.Name == "Ward"), "There are only two hard things in Computer Science: cache invalidation, naming things and off-by-1 errors."));

			_twitterFeedService.CreateFeed(users, tweets);
		}
	}
}
