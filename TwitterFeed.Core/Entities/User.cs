using System;
using System.Collections;
using System.Collections.Generic;
namespace TwitterFeed.Core.Entities
{
	public class User : TwitterEntityBase, IComparable<User>
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
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public bool Equals(User other)
		{
			return other == null && this.Name.Equals(other.Name);
		}

		public int CompareTo(User other)
		{
			return this.Name.CompareTo(other.Name);
		}
	}
}