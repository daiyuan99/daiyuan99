using Tweetinvi.Core.Extensions;
using Tweetinvi.Models.V2;

namespace TweetSampleStreamingService
{
    /// <summary>
    /// Keeps tweets statistics and reports tweet trends
    /// </summary>
    public class TweetTrendReport : ITweetTrendReport
    {
        private readonly object _lockTotal = new object();
        private readonly object _lockTrend = new object();
        private readonly object _lockHasTags = new object();
        private readonly object _lockLastTweet = new object();

        private Dictionary<string, int> _hashTags = new Dictionary<string, int>();

        private long _totalTweets;

        private DateTime _startTime;
        private DateTime _lastTimeTweetsAdded;
        private TweetV2 _lastReceivedTweet;

        /// <summary>
        /// Constructor
        /// </summary>
        public TweetTrendReport()
        {
            _startTime = DateTime.Now;
        }
        /// <summary>
        /// Total number of tweents as of request
        /// </summary>
        public long TotalTweets { get => _totalTweets; }
        /// <summary>
        /// Returns top n(default 10) OF hasg tags with counts as of request
        /// </summary>
        /// <param name="numberOfTags"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetTopHashTags(int numberOfTags = 10)
        {
            lock (_lockHasTags)
            {
                var tags = new Dictionary<string, int>();
                var cnt = 1;
                foreach (var tag in _hashTags.OrderByDescending(key => key.Value))
                {
                    if (cnt > numberOfTags) return tags;
                    tags.Add(tag.Key, tag.Value);
                    cnt++;
                }
                return tags;
            }
        }
        /// <summary>
        /// Add new tweet
        /// </summary>
        /// <param name="tweet"></param>
        /// <returns></returns>
        public void NewTweet(TweetV2 tweet)
        {
            lock (_lockTotal)
            {
                _totalTweets++;
                _lastTimeTweetsAdded = DateTime.Now;       
                _lastReceivedTweet = tweet;
            }

            
            UpdateHasTags(tweet);
        }
        /// <summary>
        /// Last received tweet
        /// </summary>
        public TweetV2 LastReceivedTweet
        {
            get => _lastReceivedTweet;

            set
            {
                lock (_lockLastTweet)
                {
                    _lastReceivedTweet = value;
                }
            }
        }

        private void UpdateHasTags(TweetV2 tweet)
        {
            if(tweet == null || tweet.Entities == null || tweet.Entities.Hashtags == null) return;
            
            tweet.Entities.Hashtags.ForEach(x =>
            {
                lock (_lockTrend)
                {
                    if (_hashTags.ContainsKey(x.Tag))
                        _hashTags[x.Tag]++;
                    else
                        _hashTags[x.Tag] = 1;
                }
            });
        }
    }
}
