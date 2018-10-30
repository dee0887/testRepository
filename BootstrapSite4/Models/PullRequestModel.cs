using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.Model
{
    /// <summary>
    /// Модель пулл-реквеста
    /// </summary>
    public class PullRequestModel : AttachmentAbstractModel
    {
        public PullRequestModel(string key, string slug) : base(key, slug) { }
        public UserModel Author { get; set; }
        public string PullRequestNumber { get; set; }
        public string PullRequestTitle { get; set; }
        public string PullRequestDate { get; set; }
        public string PullRequestUrl { get; set; }

        public List<string> Reviewer { get; set; }
        public List<ActivitiesModel> Actives { get; set; }
        List<UserModel> Reviewers { get; set; }
        public override HtmlString GetAttachment()
        {
            if (!this.IsAttachment())
                return new HtmlString("");
            Uri uri = new Uri(string.Format(
                "{0}/projects/{1}/repos/{2}/attachments{3}",
                Helper.ConfigHelper.get().BitbucketUri,
                this._key,
                this._slug,
                this.Description.Substring(this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")), this.Description.LastIndexOf(')') - this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")))
               ));
            return new HtmlString(string.Format("    <img style=\"max-width:auto;  max-height:auto;  \" class=\" \" src=\"{0}\" >", uri));
        }
    }
}