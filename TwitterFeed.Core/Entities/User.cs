using System.Collections.Generic;
namespace TwitterFeed.Core.Entities
{
	public class User : TwitterEntityBase
	{
		public string Name { get; set; }
		public List<User> Followers { get; set; }
	}
}