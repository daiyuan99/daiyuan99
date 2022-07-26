using Tweetinvi;
using Tweetinvi.Models.V2;
using Tweetinvi.Streaming.V2;

namespace TweetSampleStreamingService
{
    /// <summary>
    /// Defines class that will streaming tweets and process them to generate trend report
    /// </summary>
    public class SampleStreamingService : ISampleStreamingService
    {
        private readonly ITweetTrendReport _tweetTrendReport;
        private readonly IServiceLogger _serviceLogger;
        private ITwitterClient _tweetinviTestClient;
        private ISampleStreamV2 _stream;
        private bool _isStreamRunning;

        /// <summary>
        /// Create an instance with injected objects
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="tweetTrendReport"></param>
        /// <param name="serviceLogger"></param>
        public SampleStreamingService(ITwitterClient twitterClient, ITweetTrendReport tweetTrendReport, IServiceLogger serviceLogger)
        {
            this._tweetTrendReport = tweetTrendReport;
            this._serviceLogger = serviceLogger;

            _tweetinviTestClient = twitterClient;

        }

        /// <summary>
        /// Start the tweet sample streaming async
        /// </summary>
        /// <returns></returns>
        public async Task StartSampleStreamingAsync()
        {
            if (_isStreamRunning) return;

            try
            {
                _stream = _tweetinviTestClient.StreamsV2.CreateSampleStream();
                var tweetCount = 0;

                TweetV2 tweet;

                _stream.TweetReceived += (sender, args) =>
                {
                    tweet = args.Tweet;
                    if(tweet == null)
                    {
                        _serviceLogger.LogData(LogCategory.Error, "StartSampleStreamingAsync:TweetRecived", $"Tweet is not downloaded. Reason: {args.Json}");

                        if(args.Json.Contains("403") || args.Json.Contains("429")) //error
                        {
                            _stream.StopStream();
                            _isStreamRunning = false;
                        }    
                    }
                    else
                    {
                        //run the adding tweet in thread pool. Don't wait on the task because the sample streaming is kept alive
                        Task.Run(() => _tweetTrendReport.NewTweet(tweet));
                    }

                    Console.WriteLine($"No.{tweetCount} tweet: {tweet}");
                };

                _serviceLogger.LogData(LogCategory.Information, "", "Start tweet sample streaming...");

                _isStreamRunning = true;

                await _stream.StartAsync();

                _serviceLogger.LogData(LogCategory.Information, "", "Done with tweet sample streaming...");

            }
            catch (Exception ex)
            {
                _serviceLogger.LogData(LogCategory.Error, "SampleStreamingService:StartSampleStreamingAsync()", $"Error: {ex.Message}  Stacktrace: {ex.StackTrace ?? "No stack trace"}");
            }
        }
        /// <summary>
        /// Stop the sample stream
        /// </summary>
        public void StopStream()
        {
            if( !_isStreamRunning) return;

            _serviceLogger.LogData(LogCategory.Information, "StopStream", "Stop sample streaming..");
            _stream.StopStream();            
            _isStreamRunning = false;
            _serviceLogger.LogData(LogCategory.Information, "StopStream", "Sample streaming is stopped.");
        }

        /// <summary>
        /// Is Stream running
        /// </summary>
        /// <returns></returns>
        public bool IsStreamRunning()
        {
            return _isStreamRunning;
        }

    }
}