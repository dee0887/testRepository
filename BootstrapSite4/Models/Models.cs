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
    public class PullRequestModel : Attachment
    {
        public PullRequestModel(string key, string slug) : base(key, slug) { }
        public User Author { get; set; }
        public string PullRequestNumber { get; set; }
        public string PullRequestTitle { get; set; }
        public string PullRequestDate { get; set; }
        public string PullRequestUrl { get; set; }

        public List<string> Reviewer { get; set; }
        public List<Activities> Actives { get; set; }
        List<User> Reviewers { get; set; }
        public override HtmlString GetAttachment()
        {
            if (!this.IsAttachment())
                return new HtmlString("");
            Uri uri = new Uri(string.Format(
                "{0}/projects/{1}/repos/{2}/attachments{3}",
                BootstrapSite4.Controllers.HomeController.BitbucketUri,
                this._key,
                this._slug,
                this.Description.Substring(this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")), this.Description.LastIndexOf(')') - this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")))
               ));
            return new HtmlString(string.Format("    <img style=\"max-width:auto;  max-height:auto;  \" class=\" \" src=\"{0}\" >", uri));
        }
    }
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Slug { get; set; }
    }
    public class Activities : Attachment
    {
        public Activities(string key, string slug) : base(key, slug) { }
        public User Author { get; set; }
        public string Action { get; set; }

        public string Date { get; set; }


        public override HtmlString GetAttachment()
        {
            if (!this.IsAttachment())
                return new HtmlString(this.GetDescriptionFormatted());
            Uri uri = new Uri(string.Format(
                "{0}/projects/{1}/repos/{2}/attachments{3}",
                BootstrapSite4.Controllers.HomeController.BitbucketUri,
                this._key,
                this._slug,
                this.Description.Substring(this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")), this.Description.LastIndexOf(')') - this.Description.IndexOf('/', this.Description.LastIndexOf("attachment:")))
               ));
            return new HtmlString(string.Format("<a href=\"{0}\">{1}</a>", uri, this.GetDescriptionFormatted()));
        }
    }
    public abstract class Attachment
    {
        protected string _key; protected string _slug;
        public Attachment(string key, string slug) { this._key = key; this._slug = slug; }
        public string Description { get; set; }
        public bool IsAttachment()
        {
            return !string.IsNullOrWhiteSpace(this.Description) && this.Description.Contains("attachment:");
        }
        protected string GetDescriptionFormatted()
        {
            if (!this.IsAttachment())
                return this.Description;
            return this.Description.Remove(this.Description.LastIndexOf(']') + 1);
        }
        public abstract HtmlString GetAttachment();
    }
}