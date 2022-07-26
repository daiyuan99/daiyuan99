namespace TweetSampleStreamingService
{
    /// <summary>
    /// Defines interface that will streaming tweets and process them to generate trend report
    /// </summary>
    public interface ISampleStreamingService
    {
        /// <summary>
        /// Start the tweet sample streaming async
        /// </summary>
        /// <returns></returns>
        Task StartSampleStreamingAsync();

        /// <summary>
        /// Stop the sample stream
        /// </summary>
        void StopStream();

        /// <summary>
        /// Is Stream running
        /// </summary>
        /// <returns></returns>
        bool IsStreamRunning();
    }
}