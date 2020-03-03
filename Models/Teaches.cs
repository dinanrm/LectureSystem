using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class Teaches
    {
        public Teaches()
        {
            Status = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
        public int TeachId { get; set; }
        public int ClassScheduleId { get; set; }
        public int LecturerId { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ClassSchedules ClassSchedule { get; set; }
        public virtual Lecturers Lecturer { get; set; }
    }
}
