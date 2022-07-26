namespace TweetSampleStreamingService
{

    public interface IServiceLogger
    {
        /// <summary>
        /// Returns all current logs
        /// </summary>
        /// <returns></returns>
        List<LogInfo> CurrentLogs();

        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="logCategory">Category of info</param>
        /// <param name="source">The originator of the infor</param>
        /// <param name="message">The detail information to log</param>
        void LogData(LogCategory logCategory, string source, string message);

        /// <summary>
        /// Last Error
        /// </summary>
        /// <returns></returns>
        LogInfo LastError();


        /// <summary>
        /// Last Error
        /// </summary>
        /// <returns></returns>
        LogInfo LastWarning();
       
    }
}