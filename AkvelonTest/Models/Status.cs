using System;
using System.Collections.Generic;

namespace AkvelonTest.Models
{
    public partial class Status
    {
        public Status()
        {
            Projects = new HashSet<Project>();
            Tasks = new HashSet<Task>();
        }

        public int StatusId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
