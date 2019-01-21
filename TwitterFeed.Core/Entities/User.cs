using System;
using System.Collections;
using System.Collections.Generic;
namespace TwitterFeed.Core.Entities
{
	public class User : IComparable<User>
	{
		public string Name { get; set; }
		public List<User> Followers { get; set; }

		public User()
		{
			Followers = new List<User>();
		}
		public User(string name)
		{
			Name = name;
			Followers = new List<User>();
		}

		public int CompareTo(User other)
		{
			return this.Name.CompareTo(other.Name);
		}
	}
}