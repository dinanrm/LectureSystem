using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class Takes
    {
        public Takes()
        {
            CourseScores = new HashSet<CourseScores>();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
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

        [JsonIgnore]
        public virtual ICollection<CourseScores> CourseScores { get; set; }
    }
}
