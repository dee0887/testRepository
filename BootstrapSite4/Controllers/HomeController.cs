using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            JObject o = JObject.Parse(client.Execute(request).Content);
            if (o["values"] == null) return string.Empty;
            return o["values"].First["slug"].Value<string>();
        }

        private List<ProjectModel> GetProjects(string key)
        {
            List<ProjectModel> output = new List<ProjectModel>();
            var client = new RestClient(BitbucketUri) { Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(BitbucketLogin, BitbucketPassword) };
            var request = new RestRequest("/rest/api/1.0/projects", Method.GET);
            JObject o = JObject.Parse(client.Execute(request).Content);
            if (o["values"] == null) return output;
            foreach (var pro in o["values"])
            {
                ProjectModel p = new ProjectModel();
                p.Description = pro["description"]==null?string.Empty:pro["description"].Value<string>();
                p.Key = pro["key"].Value<string>();
                p.Name = pro["name"].Value<string>();
                if (pro["links"] != null)
                    p.Link = pro["links"]["self"].First["href"].Value<string>();
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
            JObject o = JObject.Parse(client.Execute(request).Content);
            if (o["values"] == null) return output;
            foreach (var pul in o["values"])
            {
                PullRequestModel p = new PullRequestModel(key,slug);
                p.Author = new User()
                {
                    Name = pul["author"]["user"]["displayName"].Value<string>(),
                    Email = pul["author"]["user"]["emailAddress"].Value<string>(),
                    Slug = pul["author"]["user"]["slug"].Value<string>(),
                };
                p.PullRequestUrl = pul["links"]["self"].First["href"].Value<string>();
                p.PullRequestNumber = pul["id"].Value<string>();
                p.PullRequestTitle = pul["title"].Value<string>();
                p.PullRequestDate = convertUnixDT(pul["createdDate"].Value<long>()).ToString("dd.MM.yyyy HH:mm:ss");
                p.Description = pul["description"]==null? string.Empty: pul["description"].Value<string>();
                output.Add(p);
            }
            return output;
        }
        private void GetPullRequestActivities(PullRequestModel pullrequest, string key, string slug)
        {
            pullrequest.Actives = new List<Activities>();
            var client = new RestClient(BitbucketUri) { Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(BitbucketLogin, BitbucketPassword) };
            var request = new RestRequest(string.Format("rest/api/1.0/projects/{1}/repos/{2}/pull-requests/{0}/activities", pullrequest.PullRequestNumber, key, slug), Method.GET);
            JObject o = JObject.Parse(client.Execute(request).Content);
          
            foreach (var act in o["values"])
            {
                Activities a = new Activities(key, slug);
                a.Author = new User() { Name = act["user"]["displayName"].Value<string>(), Email = act["user"]["emailAddress"].Value<string>(), Slug = act["user"]["slug"].Value<string>() };
                a.Action = act["action"].Value<string>();
                a.Description = act["comment"] != null ? act["comment"]["text"].Value<string>() : act["commit"] !=null ? act["commit"]["message"].Value<string>() : string.Empty;
                a.Date = convertUnixDT(act["createdDate"].Value<long>()).ToString("dd.MM.yyyy HH:mm:ss");

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
                model.pullrequests = GetPullRequest(id, GetSlug(id), 0, 20);
                foreach (PullRequestModel pr in model.pullrequests)
                    GetPullRequestActivities(pr, id, GetSlug(id));
            }
            return View("index", model);

        }

        [HttpGet]

        public ActionResult lazyload(string key, string rowskip)
        {
            List<PullRequestModel> model = GetPullRequest(key, GetSlug(key), int.Parse(rowskip), 20);
            foreach (PullRequestModel pr in model)
                GetPullRequestActivities(pr, key, GetSlug(key));
            return View("Partials/ContentPartial", model);
        }
    }
}