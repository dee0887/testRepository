using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.ApiModels.PullRequests
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

    public class Repository
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

    public class FromRef
    {
        public string id { get; set; }
        public string displayId { get; set; }
        public string latestCommit { get; set; }
        public Repository repository { get; set; }
    }

    public class Self3
    {
        public string href { get; set; }
    }

    public class Links3
    {
        public List<Self3> self { get; set; }
    }

    public class Project2
    {
        public string key { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool @public { get; set; }
        public string type { get; set; }
        public Links3 links { get; set; }
    }

    public class Clone2
    {
        public string href { get; set; }
        public string name { get; set; }
    }

    public class Self4
    {
        public string href { get; set; }
    }

    public class Links4
    {
        public List<Clone2> clone { get; set; }
        public List<Self4> self { get; set; }
    }

    public class Repository2
    {
        public string slug { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string scmId { get; set; }
        public string state { get; set; }
        public string statusMessage { get; set; }
        public bool forkable { get; set; }
        public Project2 project { get; set; }
        public bool @public { get; set; }
        public Links4 links { get; set; }
    }

    public class ToRef
    {
        public string id { get; set; }
        public string displayId { get; set; }
        public string latestCommit { get; set; }
        public Repository2 repository { get; set; }
    }

    public class Self5
    {
        public string href { get; set; }
    }

    public class Links5
    {
        public List<Self5> self { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
        public int id { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public Links5 links { get; set; }
    }

    public class Author
    {
        public User user { get; set; }
        public string role { get; set; }
        public bool approved { get; set; }
        public string status { get; set; }
    }

    public class Properties
    {
        public int resolvedTaskCount { get; set; }
        public int openTaskCount { get; set; }
    }

    public class Self6
    {
        public string href { get; set; }
    }

    public class Links6
    {
        public List<Self6> self { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public int version { get; set; }
        public string title { get; set; }
        public string state { get; set; }
        public bool open { get; set; }
        public bool closed { get; set; }
        public long createdDate { get; set; }
        public long updatedDate { get; set; }
        public long closedDate { get; set; }
        public FromRef fromRef { get; set; }
        public ToRef toRef { get; set; }
        public bool locked { get; set; }
        public Author author { get; set; }
        public List<object> reviewers { get; set; }
        public List<object> participants { get; set; }
        public Properties properties { get; set; }
        public Links6 links { get; set; }
        public string description { get; set; }
    }

    public class PullRequests
    {
        public int size { get; set; }
        public int limit { get; set; }
        public bool isLastPage { get; set; }
        public List<Value> values { get; set; }
        public int start { get; set; }
    }
}