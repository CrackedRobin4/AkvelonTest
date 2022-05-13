using System;
using System.Collections.Generic;

namespace AkvelonTest.Models
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public string? Name { get; set; }
        public int? StatusId { get; set; }
        public int? ProjectId { get; set; }
        public int? Priority { get; set; }
        public string? Description { get; set; }

        public virtual Project? Project { get; set; }
        public virtual Status? Status { get; set; }
    }
}
