using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.ApiModels.Projects
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public List<Self> self { get; set; }
    }

    public class Value
    {
        public string key { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool @public { get; set; }
        public string type { get; set; }
        public Links links { get; set; }
    }

    public class Projects
    {
        public int size { get; set; }
        public int limit { get; set; }
        public bool isLastPage { get; set; }
        public List<Value> values { get; set; }
        public int start { get; set; }
    }
}