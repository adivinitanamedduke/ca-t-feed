using System;
using System.Collections.Generic;
using System.Text;
using TwitterFeed.Core.Entities;

namespace TwitterFeed.Core.Factories
{
	public static class UserFactory
	{
		public static User CreateUser(string name)
		{
			return new User(name.Trim());
		}
	}
}
