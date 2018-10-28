using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4
{
    public class pageModel
    {
        public List<ProjectModel> projects { get; set; }
        public List<PullRequestModel> pullrequests { get; set; }
    }
    public class ProjectModel
    {
        public string Description { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public bool Selected { get; set; }
    }
    public class PullRequestModel
    {
        public User Author { get; set; }
        public string PullRequestNumber { get; set; }
        public string PullRequestTitle { get; set; }
        public string PullRequestDate { get; set; }
        public string PullRequestUrl { get; set; }
        public List<string> Reviewer { get; set; }
        public List<Activities> Actives { get; set; }
        public List<object> PullRequestContent { get; set; }
        List<User> Reviewers { get; set; }
    }
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Slug { get; set; }
    }
    public class Activities
    {
        string _key; string _slug;
        public Activities(string key, string slug) { this._key = key; this._slug = slug; }
        public User Author { get; set; }
        public string Action { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        bool IsAttachment()
        {
            return this.Text.Contains("attachment:");
        }
        string GetCommentHeader()
        {
            if (!this.IsAttachment())
                return this.Text;
            return this.Text.Remove(this.Text.LastIndexOf(']') + 1);
        }
        public HtmlString GetAttachmentUrl()
        {
            if (!this.IsAttachment())
                return new HtmlString(this.GetCommentHeader());
            Uri uri = new Uri(string.Format(
                "{0}/projects/{1}/repos/{2}/attachments{3}",
                BootstrapSite4.Controllers.HomeController.BitbucketUri,
                this._key,
                this._slug,
                this.Text.Substring(this.Text.IndexOf('/', this.Text.LastIndexOf("attachment:")), this.Text.LastIndexOf(')') - this.Text.IndexOf('/', this.Text.LastIndexOf("attachment:")))
               ));
            return new HtmlString(string.Format("<a href=\"{0}\">{1}</a>", uri, this.GetCommentHeader()));
        }
    }
}