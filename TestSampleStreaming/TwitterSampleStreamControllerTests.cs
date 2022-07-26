using Tweetinvi;
using TweetSampleStreamingService;
using TweetsTrendReport.Controllers;

namespace TestSampleStreaming
{
    public class TwitterSampleStreamControllerTests
    {
        //Should save these credentials to a secret place. Should not hard coded here or check in the repo. This is just for POC
        const string apiKey = "";
        const string keySecret = "";
        const string bearToken = "";
        [Fact]
        public async Task GivenTwitterSampleStreamStarted_ThenTweetsShouldBeReceivedAsync()
        {
            // Arrange
            var twiterClient = new TwitterClient(apiKey, keySecret, bearToken);
            var serviceLogger = new ServiceLogger();
            var tweetTrendReport = new TweetTrendReport();

            var streamService = new SampleStreamingService(twiterClient, tweetTrendReport, serviceLogger);

            var sut = new TwitterSampleStreamController(streamService, tweetTrendReport, serviceLogger);

            // Act

            var taskStream= sut.StartSampleStreaming();
            var delayTask = Task.Delay(TimeSpan.FromSeconds(5));

            var task = await Task.WhenAny(taskStream, delayTask); // 

            // Assert

            Assert.NotEmpty(tweetTrendReport.LastReceivedTweet.Text);
            Assert.True(tweetTrendReport.TotalTweets > 0);
            sut.StopSampleStreaming();
        }
    }
}