using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.ApiModels.Activities
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public List<Self> self { get; set; }
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
        public Links links { get; set; }
    }

    public class Properties
    {
        public int repositoryId { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public List<Self2> self { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
        public int id { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public Links2 links { get; set; }
    }

    public class PermittedOperations
    {
        public bool editable { get; set; }
        public bool deletable { get; set; }
    }

    public class Comment
    {
        public Properties properties { get; set; }
        public int id { get; set; }
        public int version { get; set; }
        public string text { get; set; }
        public Author author { get; set; }
        public object createdDate { get; set; }
        public object updatedDate { get; set; }
        public List<object> comments { get; set; }
        public List<object> tasks { get; set; }
        public PermittedOperations permittedOperations { get; set; }
    }

    public class Self3
    {
        public string href { get; set; }
    }

    public class Links3
    {
        public List<Self3> self { get; set; }
    }

    public class Author2
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
        public int id { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public Links3 links { get; set; }
    }

    public class Self4
    {
        public string href { get; set; }
    }

    public class Links4
    {
        public List<Self4> self { get; set; }
    }

    public class Committer
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
        public int id { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public Links4 links { get; set; }
    }

    public class Author3
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
    }

    public class Committer2
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
    }

    public class Parent
    {
        public string id { get; set; }
        public string displayId { get; set; }
        public Author3 author { get; set; }
        public object authorTimestamp { get; set; }
        public Committer2 committer { get; set; }
        public object committerTimestamp { get; set; }
        public string message { get; set; }
        public List<object> parents { get; set; }
    }

    public class Commit
    {
        public string id { get; set; }
        public string displayId { get; set; }
        public Author2 author { get; set; }
        public long authorTimestamp { get; set; }
        public Committer committer { get; set; }
        public long committerTimestamp { get; set; }
        public string message { get; set; }
        public List<Parent> parents { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public long createdDate { get; set; }
        public User user { get; set; }
        public string action { get; set; }
        public string commentAction { get; set; }
        public Comment comment { get; set; }
        public Commit commit { get; set; }
    }

    public class Activities
    {
        public int size { get; set; }
        public int limit { get; set; }
        public bool isLastPage { get; set; }
        public List<Value> values { get; set; }
        public int start { get; set; }
    }
}