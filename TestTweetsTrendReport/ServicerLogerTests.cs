using TweetSampleStreamingService;

namespace TestTweetsTrendReport
{
    public class ServicerLogerTests
    {
        [Fact]
        public void GivenAnErrorMessageIsLogged_ThenLastErrorShouldHaveIt()
        {
            // Arrange
            var sut = new ServiceLogger();
            var expectedLogInfo = new LogInfo(LogCategory.Error, "Test", "An error occurred");
            // Act

            sut.LogData(expectedLogInfo.Category, expectedLogInfo.Source, expectedLogInfo.Message);

            // Assert
            Assert.Equal(expectedLogInfo.Category, sut.LastError().Category);
            Assert.Equal(expectedLogInfo.Message, sut.LastError().Message);
            Assert.Equal(expectedLogInfo.Source, sut.LastError().Source);
        }

        [Fact]
        public void GiveMultipleLogs_CurrentLogsShouldHaveListOfLogs()
        {
            // Arrange
            var sut = new ServiceLogger();
            var expectedLogs = 3;
            var expectedLogInfo = new LogInfo(LogCategory.Error, "Test", "An error occurred");
            // Act
            sut.LogData(expectedLogInfo.Category, expectedLogInfo.Source, expectedLogInfo.Message);
            sut.LogData(expectedLogInfo.Category, expectedLogInfo.Source, expectedLogInfo.Message);
            sut.LogData(expectedLogInfo.Category, expectedLogInfo.Source, expectedLogInfo.Message);
            // Assert
            Assert.Equal(expectedLogs, sut.CurrentLogs().Count());
        }
    }
}