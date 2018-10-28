using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BootstrapSite4.ApiModels;

namespace BootstrapSite4.Controllers
{
    public class HomeController : Controller
    {
        public static readonly string BitbucketUri = "http://ci.corteos.ru:7990";
        readonly string BitbucketLogin = "guest";
        readonly string BitbucketPassword = "1qaz!QAZ";

        private string GetSlug(string key)
        {
            List<ProjectModel> output = new List<ProjectModel>();
            var client = new RestClient(BitbucketUri) { Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(BitbucketLogin, BitbucketPassword) };
            var request = new RestRequest(string.Format("/rest/api/1.0/projects/{0}/repos", key), Method.GET);
            BootstrapSite4.ApiModels.Repos.Repos repos = JsonConvert.DeserializeObject<BootstrapSite4.ApiModels.Repos.Repos>(client.Execute(request).Content);
            return repos.values.First().slug;
        }

        private List<ProjectModel> GetProjects(string key)
        {
            List<ProjectModel> output = new List<ProjectModel>();
            var client = new RestClient(BitbucketUri) { Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(BitbucketLogin, BitbucketPassword) };
            var request = new RestRequest("/rest/api/1.0/projects", Method.GET);
            BootstrapSite4.ApiModels.Projects.Projects projects = JsonConvert.DeserializeObject<BootstrapSite4.ApiModels.Projects.Projects>(client.Execute(request).Content);

            foreach (var pro in projects.values)
            {
                ProjectModel p = new ProjectModel();
                p.Description = pro.description;
                p.Key = pro.key;
                p.Name = pro.name;
                if (pro.links != default(BootstrapSite4.ApiModels.Projects.Links) && pro.links.self != null && pro.links.self.Any())
                    p.Link = pro.links.self.First().href;
                p.Selected = string.Equals(key, p.Key, StringComparison.OrdinalIgnoreCase);
                output.Add(p);
            }
            return output;
        }
        private DateTime convertUnixDT(long udt)
        {
            TimeZone zone = TimeZone.CurrentTimeZone;
            return zone.ToLocalTime(new DateTime(1970, 1, 1).AddMilliseconds(udt));
        }
        private List<PullRequestModel> GetPullRequest(string key, string slug, int start, int limit)
        {
            List<PullRequestModel> output = new List<PullRequestModel>();
            var client = new RestClient(BitbucketUri) { Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(BitbucketLogin, BitbucketPassword) };
            var request = new RestRequest(string.Format("rest/api/1.0/projects/{0}/repos/{1}/pull-requests?state=ALL&start={2}&limit={3}", key, slug, start, limit), Method.GET);
            BootstrapSite4.ApiModels.PullRequests.PullRequests pullrequests = JsonConvert.DeserializeObject<BootstrapSite4.ApiModels.PullRequests.PullRequests>(client.Execute(request).Content);
            foreach (var pul in pullrequests.values)
            {
                PullRequestModel p = new PullRequestModel();
                p.Author = new User()
                {
                    Name = pul.author.user.displayName,
                    Email = pul.author.user.emailAddress,
                    Slug = pul.author.user.slug
                };
                p.PullRequestUrl = pul.links.self.First().href;
                p.PullRequestNumber = pul.id.ToString();
                p.PullRequestTitle = pul.title;
                p.PullRequestDate = convertUnixDT(pul.createdDate).ToString("dd.MM.yyyy HH:mm:ss");




                output.Add(p);
            }         
            return output;
        }
        private void GetPullRequestActivities(PullRequestModel pullrequest,string key,string slug)
        {
            pullrequest.Actives = new List<Activities>();
            var client = new RestClient(BitbucketUri) { Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(BitbucketLogin, BitbucketPassword) };
            var request = new RestRequest(string.Format("rest/api/1.0/projects/{1}/repos/{2}/pull-requests/{0}/activities", pullrequest.PullRequestNumber,key,slug), Method.GET);
            BootstrapSite4.ApiModels.Activities.Activities activities = JsonConvert.DeserializeObject<BootstrapSite4.ApiModels.Activities.Activities>(client.Execute(request).Content);
            foreach (var act in activities.values)
            {
                Activities a = new Activities(key,slug) ;
                a.Author = new User() { Name = act.user.displayName, Email = act.user.emailAddress, Slug = act.user.slug };
                a.Action = act.action;
                a.Text = act.comment != default(BootstrapSite4.ApiModels.Activities.Comment) ? act.comment.text : act.commit != default(BootstrapSite4.ApiModels.Activities.Commit) ? act.commit.message : string.Empty;
                a.Date = convertUnixDT(act.createdDate).ToString("dd.MM.yyyy HH:mm:ss");

                pullrequest.Actives.Add(a);
            }
        }
        public ActionResult Index()
        {
            pageModel model = new pageModel() { projects = GetProjects(null) };
            return View(model);

        }
        public ActionResult Project(string id)
        {            
            pageModel model = new pageModel() { projects = GetProjects(id) };
            if (model.projects.Where(w => w.Selected).Any())
            {
                model.pullrequests=GetPullRequest(id, GetSlug(id), 0, 20);
                foreach (PullRequestModel pr in model.pullrequests)
                    GetPullRequestActivities(pr,id, GetSlug(id));
            }
            return View("index", model);

        }

        [HttpGet]

        public ActionResult lazyload(string key,string rowskip)
        {
           
              
                List<PullRequestModel> model = GetPullRequest(key, GetSlug(key), int.Parse(rowskip), 20);
                foreach (PullRequestModel pr in model)
                    GetPullRequestActivities(pr,key, GetSlug(key));
                return View("Partials/ContentPartial", model);

                
        }
        
    }
}