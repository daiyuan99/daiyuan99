namespace TweetSampleStreamingService
{
    public interface IAccountCredentials
    {
        /// <summary>
        /// API Key
        /// </summary>
        string APIKey { get; set; }
        /// <summary>
        /// KeySecret
        /// </summary>
        string KeySecret { get; set; }
        /// <summary>
        /// Bear Token
        /// </summary>
        string BearToken { get; set; }
    }
}