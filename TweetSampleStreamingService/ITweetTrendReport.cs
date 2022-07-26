using Tweetinvi.Core.Models;
using Tweetinvi.Models.V2;

namespace TweetSampleStreamingService
{
    /// <summary>
    ///  Keeps tweets statistics and reports tweet trends
    /// </summary>
    public interface ITweetTrendReport
    {
        /// <summary>
        /// Total number of tweents as of reqyest
        /// </summary>
        long TotalTweets { get; }
        /// <summary>
        /// Returns top n(default 10) OF hasg tags with counts as of request
        /// </summary>
        /// <param name="numberOfTags"></param>
        /// <returns></returns>
        Dictionary<string, int> GetTopHashTags(int numberOfTags = 10);
        /// <summary>
        /// Add new tweet
        /// </summary>
        /// <param name="tweet"></param>
        /// <returns></returns>
        void NewTweet(TweetV2 tweet);

        /// <summary>
        /// Last received tweet
        /// </summary>
        TweetV2 LastReceivedTweet { get; set; }
    }
}