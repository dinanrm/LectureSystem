﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class ClassSchedules
    {
        public ClassSchedules()
        {
            Attendances = new HashSet<Attendances>();
            Teaches = new HashSet<Teaches>();
            Status = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public int ClassScheduleId { get; set; }
        public int CourseId { get; set; }
        public int ClassroomId { get; set; }
        public string Day { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Classrooms Classroom { get; set; }
        public virtual Courses Course { get; set; }

        public virtual ICollection<Attendances> Attendances { get; set; }
        public virtual ICollection<Teaches> Teaches { get; set; }
    }
}
