using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Services.FileParser
{
	public interface IFileParserService
	{
		List<User> ParseUser(StreamReader streamReader);
		List<Tweet> ParseTweet(StreamReader streamReader, List<User> users);

	}
}
