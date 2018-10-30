using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BootstrapSite4.Helper
{
    /// <summary>
    /// Объект предоставляющий конфигурационные данные
    /// </summary>
    class ConfigHelper
    {
        private static ConfigHelper instance;

        private ConfigHelper()
        {
            this._BitbucketUri = ConfigurationManager.AppSettings["BitbucketUri"];
            this._BitbucketLogin = ConfigurationManager.AppSettings["BitbucketLogin"];
            this._BitbucketPassword = ConfigurationManager.AppSettings["BitbucketPassword"];
            string _PullRequestsLimitString = ConfigurationManager.AppSettings["PullRequestsLimit"];
            int temp = 0;
            if (!string.IsNullOrWhiteSpace(_PullRequestsLimitString) && int.TryParse(_PullRequestsLimitString, out temp) && temp > 0)
                this._PullRequestsLimit = temp;
        }

        public static ConfigHelper get()
        {
            if (instance == null)
                instance = new ConfigHelper();
            return instance;
        }
        string _BitbucketUri = string.Empty;
        /// <summary>
        //// BitBucket URL
        /// </summary>
        public string BitbucketUri { get { return _BitbucketUri; } }
        string _BitbucketLogin = string.Empty;
        /// <summary>
        ///  BitBucket Login
        /// </summary>
        public string BitbucketLogin { get { return _BitbucketLogin; } }
        string _BitbucketPassword = string.Empty;
        /// <summary>
        /// BitBucket Password
        /// </summary>
        public string BitbucketPassword { get { return _BitbucketPassword; } }
        int _PullRequestsLimit = 20;
        /// <summary>
        /// Количество пулл-реквестов при ленивой загрузке
        /// </summary>
        public int PullRequestsLimit { get { return _PullRequestsLimit; } }
    }
}