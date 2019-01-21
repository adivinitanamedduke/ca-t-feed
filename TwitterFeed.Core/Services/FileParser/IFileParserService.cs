using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Services.FileParser
{
	public interface IFileParserService
	{
		List<User> ParseUser(string filePath);
		List<Tweet> ParseTweet(string filePath, List<User> users);

	}
}
