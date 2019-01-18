using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TwitterFeed.Core.Entities;
using TwitterFeed.Core.Logging;

namespace TwitterFeed.Core.Services.FileParser
{
	public class FileParserService : IFileParserService
    {
		private readonly ILogger _logger;
		public FileParserService (ILogger logger)
		{
			_logger = logger;
		}

		public List<TwitterEntityBase> Parse(string filePath)
		{
			try
			{
				using (StreamReader file = new System.IO.StreamReader(filePath))
				{
					//TODO: File processing
				}

			}
			catch (IOException ex)
			{
				_logger.Error(ex);
			}

			return new List<TwitterEntityBase>();
		}
	}
}
