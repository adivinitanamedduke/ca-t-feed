namespace TwitterFeed.Core.Entities
{
	public class Tweet : TwitterEntityBase
	{
		public string Message { get; set; }

		public User User { get; set; }

		public Tweet(User user, string message)
		{
			User = user;
			Message = message;
		}

	}
}