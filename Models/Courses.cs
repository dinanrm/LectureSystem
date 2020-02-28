using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class Courses
    {
        public Courses()
        {
            ClassSchedules = new HashSet<ClassSchedules>();
            Fields = new HashSet<Fields>();
            Files = new HashSet<Files>();
            Takes = new HashSet<Takes>();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SemesterCreditUnit { get; set; }
        public string Curriculum { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<ClassSchedules> ClassSchedules { get; set; }
        [JsonIgnore]
        public virtual ICollection<Fields> Fields { get; set; }
        [JsonIgnore]
        public virtual ICollection<Files> Files { get; set; }
        [JsonIgnore]
        public virtual ICollection<Takes> Takes { get; set; }
    }
}
