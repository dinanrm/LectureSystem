using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Takes
    {
        public Takes()
        {
            CourseScores = new HashSet<CourseScores>();
        }

        public int TakeId { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string AcademicYear { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Semesters Semester { get; set; }
        public virtual Students Student { get; set; }
        public virtual ICollection<CourseScores> CourseScores { get; set; }
    }
}
