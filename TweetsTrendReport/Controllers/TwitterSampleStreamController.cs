using Microsoft.AspNetCore.Mvc;
using System.Text;
using TweetSampleStreamingService;


namespace TweetsTrendReport.Controllers
{
    /// <summary>
    /// Defines actions for twitter sample streaming
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterSampleStreamController : ControllerBase
    {
        private readonly ISampleStreamingService sampleStreamingService;
        private readonly ITweetTrendReport tweetTrendReport;
        private readonly IServiceLogger serviceLogger;

        public TwitterSampleStreamController(ISampleStreamingService sampleStreamingService, ITweetTrendReport tweetTrendReport, IServiceLogger serviceLogger)
        {
            this.sampleStreamingService = sampleStreamingService;
            this.tweetTrendReport = tweetTrendReport;
            this.serviceLogger = serviceLogger;
        }

        /// <summary>
        /// Start the stream
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Start")]
        public async Task<string> StartSampleStreaming()
        {
            if (sampleStreamingService.IsStreamRunning())
                return "Stream is running already.";
            else
            {
                var taskStream = sampleStreamingService.StartSampleStreamingAsync();
                var delayTask = Task.Delay(TimeSpan.FromSeconds(5));

                var task = await Task.WhenAny(taskStream, delayTask);

                return "Stream is started.";
            }            
        }
        /// <summary>
        /// Stop the stream
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Stop")]
        public string StopSampleStreaming()
        {
            if (sampleStreamingService.IsStreamRunning())
            {
                sampleStreamingService.StopStream();
                return "Stream is stopped.";
            }
            else
                return "Stream is not started yet.";
                
        }
        /// <summary>
        /// Get tweet trend
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("TweetTrend")]
        public string GetTweetTrend()
        {
            var trend = new StringBuilder();
            trend.AppendLine( $"Total tweets received: {tweetTrendReport.TotalTweets}");
            trend.AppendLine();

            trend.AppendLine("Top 10 tags are: ");
            int cnt = 1;
            foreach(var item in tweetTrendReport.GetTopHashTags())
            {
                trend.AppendLine($"[#{item.Key}] : ({item.Value}) || ");
                cnt++;
                trend.AppendLine();
            }
            if (cnt == 1) trend.AppendLine("No hash tag found so far!");

            return trend.ToString();
        }

        /// <summary>
        /// Get the most recent error(s)
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Errors")]
        public string GetRecentError()
        {
            var lastError = serviceLogger.LastError();
            return $"The most recent error=>Source: {  lastError.Source}  Message: { lastError.Message}";
        }

        /// <summary>
        /// Get Last Received Tweet's text
        /// </summary>
        /// <returns></returns>

        [HttpGet, Route("LastTweet")]
        public string GetLastTweet()
        {
            return tweetTrendReport.LastReceivedTweet?.Text??"N/A";
        }

    }
}
