using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetSampleStreamingService
{
    /// <summary>
    /// Defines tweet API account credentials
    /// </summary>
    public class AccountCredentials : IAccountCredentials
    {
        /// <summary>
        /// API Key
        /// </summary>
        public string APIKey { get; set; }
        /// <summary>
        /// KeySecret
        /// </summary>
        public string KeySecret { get; set; }
        /// <summary>
        /// Bear Token
        /// </summary>
        public string BearToken { get; set; }

    }
}
