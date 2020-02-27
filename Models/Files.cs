using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Files
    {
        public int FileId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public long? Size { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Courses Course { get; set; }
    }
}
