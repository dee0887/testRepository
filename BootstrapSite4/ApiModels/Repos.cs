using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.ApiModels.Repos
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public List<Self> self { get; set; }
    }

    public class Project
    {
        public string key { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool @public { get; set; }
        public string type { get; set; }
        public Links links { get; set; }
    }

    public class Clone
    {
        public string href { get; set; }
        public string name { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public List<Clone> clone { get; set; }
        public List<Self2> self { get; set; }
    }

    public class Value
    {
        public string slug { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string scmId { get; set; }
        public string state { get; set; }
        public string statusMessage { get; set; }
        public bool forkable { get; set; }
        public Project project { get; set; }
        public bool @public { get; set; }
        public Links2 links { get; set; }
    }

    public class Repos
    {
        public int size { get; set; }
        public int limit { get; set; }
        public bool isLastPage { get; set; }
        public List<Value> values { get; set; }
        public int start { get; set; }
    }
}