using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ClassSchedules ClassSchedule { get; set; }
        [JsonIgnore]
        public virtual Lecturers Lecturer { get; set; }
        [JsonIgnore]
        public virtual Students Student { get; set; }
    }
}
