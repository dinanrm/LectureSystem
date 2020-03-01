using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Fields
    {
        public Fields()
        {
            Status = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
        public int FieldId { get; set; }
        public int CourseId { get; set; }
        public int LecturerId { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Lecturers Lecturer { get; set; }
    }
}
