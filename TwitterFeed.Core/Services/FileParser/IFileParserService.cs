using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Services.FileParser
{
	public interface IFileParserService
	{
		List<TwitterEntityBase> Parse(string filePath);
	}
}
