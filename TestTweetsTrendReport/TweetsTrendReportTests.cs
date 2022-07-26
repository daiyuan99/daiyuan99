using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSampleStreamingService;

namespace TestTweetsTrendReport
{

    public class TweetsTrendReportTests
    {
        [Fact]
        public void GiveATweetAdded_ThenTweetTrendStatisticsShouldBeUpdated()
        {
            // Arrange
            var sut = new TweetTrendReport();
            var newTweet = new Tweetinvi.Models.V2.TweetV2();
            newTweet.Entities = new Tweetinvi.Models.V2.TweetEntitiesV2();
            newTweet.Entities.Hashtags = new Tweetinvi.Models.V2.HashtagV2[] { new Tweetinvi.Models.V2.HashtagV2 { Tag = "Test" } };
            // Act
            
            var task = Task.Run( () => sut.NewTweet(newTweet));

            task.Wait();

            // Assert

            Assert.Equal(newTweet.Text, sut.LastReceivedTweet.Text);
            Assert.Single(sut.GetTopHashTags());
            Assert.Equal("Test", sut.GetTopHashTags().Keys.First());
            Assert.Equal(1, sut.TotalTweets);
        }
    }
}
