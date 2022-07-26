namespace TweetSampleStreamingService
{
    /// <summary>
    /// Defines Log categories
    /// </summary>
    public enum LogCategory
    {
        Information = 0,
        Warning = 1,
        Error = 2,
    }

    /// <summary>
    /// Class that holds a log info
    /// </summary>
    public class LogInfo
    {
       public LogInfo(LogCategory logCategory, string source, string message)
        {
            Category = logCategory;
            Source = source;
            Message = message;
            Timestamp = DateTime.Now;
        }

        public LogCategory Category { get; set; }

        public string Source { get; set; }
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }
    }
    /// <summary>
    /// Class that is capable of logging information and be retrieved later
    /// </summary>
    public class ServiceLogger : IServiceLogger
    {
        const int _maxCount = 10000;

        private object _lock = new object();

        private int _count = 0;

        private List<LogInfo> _logInfos = new List<LogInfo>();
        /// <summary>
        /// Returns all current logs
        /// </summary>
        /// <returns></returns>
        public List<LogInfo> CurrentLogs()
        {
            return _logInfos;
        }
        /// <summary>
        /// Last Error
        /// </summary>
        /// <returns></returns>
        public LogInfo LastError()
        {
            if (_logInfos.Count(y => y.Category == LogCategory.Error) == 0) return new LogInfo(LogCategory.Information,"ServiceLogger","No Log");

            var sortedList = _logInfos.Where(y => y.Category == LogCategory.Error).OrderByDescending(x => x.Timestamp).ToArray();
            return sortedList[0];
        }

        /// <summary>
        /// Last Error
        /// </summary>
        /// <returns></returns>
        public LogInfo LastWarning()
        {
            if (_logInfos.Count == 0) return new LogInfo(LogCategory.Information, "ServiceLogger", "No Log");

            var sortedList = _logInfos.Where(y=>y.Category == LogCategory.Warning).OrderByDescending(x => x.Timestamp).ToArray();
            return sortedList[0];
        }


        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="logCategory">Category of info</param>
        /// <param name="source">The originator of the infor</param>
        /// <param name="message">The detail information to log</param>
        public void LogData(LogCategory logCategory, string source, string message)
        {
            if (_count > _maxCount)
            {
                lock(_lock)
                {
                    //Need to persist logs somewhere first, then clear all current logs from the list
                    _logInfos.Clear();
                }                
            }
            lock(_lock)
            {
                _logInfos.Add(new LogInfo(logCategory, source, message));
            }
            
#if DEBUG
            Console.WriteLine($"{DateTime.Now}| {logCategory}| {message} ");
#endif
            _count++;
        }
    }
}
