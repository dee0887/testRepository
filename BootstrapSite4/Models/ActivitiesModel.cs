using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapSite4.Helper;

namespace BootstrapSite4.Model
{
    /// <summary>
    /// Модель активности пулл-реквеста
    /// </summary>
    public class ActivitiesModel : AttachmentAbstractModel
    {
        public ActivitiesModel(string key, string slug) : base(key, slug) { }
        public UserModel Author { get; set; }
        public string Action { get; set; }

        public string Date { get; set; }

        public override HtmlString GetAttachment()
        {
            if (!this.IsAttachment())
                return new HtmlString(this.GetDescriptionFormatted());
            Uri uri = new Uri(string.Format(
                "{0}/projects/{1}/repos/{2}/attachments{3}",
                ConfigHelper.get().BitbucketUri,
                this._key,
                this._slug,
                this.Description.Substring(this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")), this.Description.LastIndexOf(')') - this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")))
               ));
            return new HtmlString(string.Format("<a href=\"{0}\">{1}</a>", uri, this.GetDescriptionFormatted()));
        }
    }
}