using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class ClassSchedules
    {
        public ClassSchedules()
        {
            Attendances = new HashSet<Attendances>();
            Teaches = new HashSet<Teaches>();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public int ClassScheduleId { get; set; }
        public int CourseId { get; set; }
        public int ClassroomId { get; set; }
        public string Day { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Classrooms Classroom { get; set; }
        public virtual Courses Course { get; set; }

        [JsonIgnore]
        public virtual ICollection<Attendances> Attendances { get; set; }
        [JsonIgnore]
        public virtual ICollection<Teaches> Teaches { get; set; }
    }
}
