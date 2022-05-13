using System;
using System.Collections.Generic;

namespace AkvelonTest.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StatusId { get; set; }
        public int? Priority { get; set; }

        public virtual Status? Status { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
