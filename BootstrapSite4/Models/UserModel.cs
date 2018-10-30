
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.Model
{
    /// <summary>
    /// Модель автора изменения пулл-реквеста
    /// </summary>
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Slug { get; set; }
        public string GetUserAvatarUrl() { return string.Format("{0}/users/{1}/avatar.png", Helper.ConfigHelper.get().BitbucketUri, this.Slug); }
    }
}