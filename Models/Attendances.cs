using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Attendances
    {
        public Attendances()
        {
            Status = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
        public int AttendId { get; set; }
        public int ClassScheduleId { get; set; }
        public int LecturerId { get; set; }
        public int StudentId { get; set; }
        public bool? IsAttend { get; set; }
        public string Reason { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ClassSchedules ClassSchedule { get; set; }
        public virtual Lecturers Lecturer { get; set; }
        public virtual Students Student { get; set; }
    }
}
