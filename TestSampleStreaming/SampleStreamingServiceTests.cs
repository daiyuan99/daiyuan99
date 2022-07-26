using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using TweetSampleStreamingService;

namespace TestSampleStreaming
{
    public class SampleStreamingServiceTests
    {
        //Should save these credentials to a secret place. Should not hard coded here or check in the repo. This is just for POC
        const string apiKey = "";
        const string keySecret = "";
        const string bearToken = "";
        [Fact]
        public async Task GivenTwitterSampleStreamStarted_ThenTweetsShouldBeReceived()
        {
            // Arrange
            var twiterClient = new TwitterClient(apiKey, keySecret, bearToken);
            var serviceLogger = new ServiceLogger();
            var tweetTrendReport = new TweetTrendReport();

            var sut = new SampleStreamingService(twiterClient, tweetTrendReport, serviceLogger);

            // Act

            var taskStream= sut.StartSampleStreamingAsync();

            var delayTask = Task.Delay(TimeSpan.FromSeconds(15));

            var task = await Task.WhenAny(taskStream, delayTask);

            // Assert

            Assert.NotEmpty(tweetTrendReport.LastReceivedTweet.Text);
            Assert.True(tweetTrendReport.TotalTweets > 0);
            sut.StopStream();
        }
    }
}
