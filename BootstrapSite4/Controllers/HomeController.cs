using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapSite4.Helper;
using BootstrapSite4.Model;

namespace BootstrapSite4.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            PageModel model = ModelHelper.PopulateWithProjects();
            return View(model);
        }
        [HttpGet]
        public ActionResult Project(string id)
        {
            PageModel model = ModelHelper.PopulateWithProjectsAndPullRequests(id);
            return View("index", model);
        }
        [HttpGet]
        public ActionResult lazyload(string key, string rowskip)
        {
            List<PullRequestModel> model = ModelHelper.GetPullRequest(key, int.Parse(rowskip), ConfigHelper.get().PullRequestsLimit);
            ModelHelper.PopulatePullRequestWithActivities(model, key);
            return View("Partials/ContentPartial", model);
        }
    }
}