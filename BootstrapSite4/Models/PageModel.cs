using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.Model
{
    /// <summary>
    /// Модель страницы
    /// </summary>
    public class PageModel
    {
        public List<ProjectModel> Projects { get; set; }
        public List<PullRequestModel> PullRequests { get; set; }
        public bool IsProjectSelected() { return this.Projects != null && this.Projects.Where(w => w.Selected).Any(); }
        public string GetSelectedProjectKey() { if (this.IsProjectSelected())return this.Projects.Where(w => w.Selected).First().Key; return string.Empty; }
        public string GetSelectedProjectName() { if (this.IsProjectSelected())return this.Projects.Where(w => w.Selected).First().Name; return string.Empty;}
    }
}