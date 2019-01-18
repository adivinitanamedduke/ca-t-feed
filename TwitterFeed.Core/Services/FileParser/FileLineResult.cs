namespace TwitterFeed.Core.Services.FileParser
{
	public class FormulaResult : IFileLineResult
	{
        public FormulaResult()
        {
            ErrorAtPosition = 0;
            HasError = false;
            Message = "Valid line";
        }
        public int ErrorAtPosition { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
        
    }
}
