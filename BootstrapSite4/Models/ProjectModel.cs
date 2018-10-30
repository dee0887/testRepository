using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.Model
{
    /// <summary>
    /// Модель проекта
    /// </summary>
    public class ProjectModel
    {
        public string Description { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public bool Selected { get; set; }
    }
}