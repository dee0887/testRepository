using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BootstrapSite4.Model;

namespace BootstrapSite4.Helper
{
    /// <summary>
    ////Методы генерации моделей
    /// </summary>
    public static class ModelHelper
    {
        /// <summary>
        /// Генерирует модель с проектами
        /// </summary>
        /// <returns>модель с проектами</returns>
        public static PageModel PopulateWithProjects()
        {
            return new PageModel() { Projects = GetProjects(null) };
        }
        /// <summary>
        /// Генерирует модель с проектами и пулл-реквестами по выбранному проекту
        /// </summary>
        /// <param name="key">ключ проекта</param>
        /// <returns>модель с проектами</returns>
        public static PageModel PopulateWithProjectsAndPullRequests(string key)
        {
            PageModel model = new PageModel() { Projects = GetProjects(key) };
            if (model.IsProjectSelected())
            {
                model.PullRequests = GetPullRequest(key, 0, ConfigHelper.get().PullRequestsLimit);
                PopulatePullRequestWithActivities(model.PullRequests, key);
            }
            return model;
        }
        /// <summary>
        /// Генерирует модели с пулл-реквестами по выбранному проекту
        /// </summary>
        /// <param name="key">ключ проекта</param>
        /// <param name="start">номер первого пулл-реквеста</param>
        /// <param name="limit">количество возвращаемых пулл-реквестов</param>
        /// <returns>список моделей пулл-реквестов</returns>
        public static List<PullRequestModel> GetPullRequest(string key, int start, int limit)
        {
            List<PullRequestModel> output = new List<PullRequestModel>();
            string slug = GetSlug(key);
            JObject o = ApiRequestGet(string.Format("rest/api/1.0/projects/{0}/repos/{1}/pull-requests?state=ALL&start={2}&limit={3}", key, slug, start, limit));
            if (o["values"] == null) return output;
            foreach (var pul in o["values"])
            {
                PullRequestModel p = new PullRequestModel(key, slug);
                p.Author = new UserModel()
                {
                    Name = pul["author"]["user"]["displayName"].Value<string>(),
                    Email = pul["author"]["user"]["emailAddress"].Value<string>(),
                    Slug = pul["author"]["user"]["slug"].Value<string>(),
                };
                p.PullRequestUrl = pul["links"]["self"].First["href"].Value<string>();
                p.PullRequestNumber = pul["id"].Value<string>();
                p.PullRequestTitle = pul["title"].Value<string>();
                p.PullRequestDate = ConvertUnixDT(pul["createdDate"].Value<long>()).ToString("dd.MM.yyyy HH:mm:ss");
                p.Description = pul["description"] == null ? string.Empty : pul["description"].Value<string>();
                output.Add(p);
            }
            return output;
        }
        /// <summary>
        ////Обогащает пулл-реквесты активностью
        /// </summary>
        /// <param name="pullrequests">пулл-реквесты</param>
        /// <param name="key">ключ проекта</param>
        public static void PopulatePullRequestWithActivities(List<PullRequestModel> pullrequests, string key)
        {
            pullrequests.ForEach(f => GetPullRequestActivities(f, key));
        }

        /// <summary>
        /// Запрос данных через API
        /// </summary>
        /// <param name="url">Url c параметрами запроса</param>
        /// <returns>Объект с результатом запроса</returns>
        static JObject ApiRequestGet(string url)
        {
            var client = new RestClient(ConfigHelper.get().BitbucketUri) { Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(ConfigHelper.get().BitbucketLogin, ConfigHelper.get().BitbucketPassword) };
            var request = new RestRequest(url, Method.GET);
            return JObject.Parse(client.Execute(request).Content);
        }
        /// <summary>
        /// Имя репозитория, в котором находится проект
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Имя репозитория</returns>
        static string GetSlug(string key)
        {
            JObject o = ApiRequestGet(string.Format("/rest/api/1.0/projects/{0}/repos", key));
            if (o["values"] == null) return string.Empty;
            return o["values"].First["slug"].Value<string>();
        }
        /// <summary>
        /// Генерирует список проектов
        /// </summary>
        /// <param name="key">ключ выбранного проекта</param>
        /// <returns>Список проектов</returns>
        static List<ProjectModel> GetProjects(string key)
        {
            List<ProjectModel> output = new List<ProjectModel>();
            JObject o = ApiRequestGet("/rest/api/1.0/projects");
            if (o["values"] == null) return output;
            foreach (var pro in o["values"])
            {
                ProjectModel p = new ProjectModel();
                p.Description = pro["description"] == null ? string.Empty : pro["description"].Value<string>();
                p.Key = pro["key"].Value<string>();
                p.Name = pro["name"].Value<string>();
                if (pro["links"] != null)
                    p.Link = pro["links"]["self"].First["href"].Value<string>();
                p.Selected = string.Equals(key, p.Key, StringComparison.OrdinalIgnoreCase);
                output.Add(p);
            }
            return output;
        }
        /// <summary>
        /// Конвертирует время из unix-формата в DateTime
        /// </summary>
        /// <param name="udt">unix формат предствление времени</param>
        /// <returns>DateTime объект</returns>
        static DateTime ConvertUnixDT(long udt)
        {
            TimeZone zone = TimeZone.CurrentTimeZone;
            return zone.ToLocalTime(new DateTime(1970, 1, 1).AddMilliseconds(udt));
        }
        /// <summary>
        /// Обогащает пулл-реквест активностью
        /// </summary>
        /// <param name="pullrequest">пулл-реквест</param>
        /// <param name="key">ключ проекта</param>
        static void GetPullRequestActivities(PullRequestModel pullrequest, string key)
        {
            pullrequest.Actives = new List<ActivitiesModel>();
            string slug = GetSlug(key);
            JObject o = ApiRequestGet(string.Format("rest/api/1.0/projects/{1}/repos/{2}/pull-requests/{0}/activities", pullrequest.PullRequestNumber, key, slug));
            foreach (var act in o["values"])
            {
                ActivitiesModel a = new ActivitiesModel(key, slug);
                a.Author = new UserModel() { Name = act["user"]["displayName"].Value<string>(), Email = act["user"]["emailAddress"].Value<string>(), Slug = act["user"]["slug"].Value<string>() };
                a.Action = act["action"].Value<string>();
                a.Description = act["comment"] != null ? act["comment"]["text"].Value<string>() : act["commit"] != null ? act["commit"]["message"].Value<string>() : string.Empty;
                a.Date = ConvertUnixDT(act["createdDate"].Value<long>()).ToString("dd.MM.yyyy HH:mm:ss");
                pullrequest.Actives.Add(a);
            }
        }
    }
}