namespace TwitterFeed.Core.Services.FileParser
{
	public interface IFileLineResult
    {
        int ErrorAtPosition { get; set; }
        bool HasError { get; set; }
        string Message { get; set; }
    }
}