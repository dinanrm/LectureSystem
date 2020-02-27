using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Classrooms
    {
        public Classrooms()
        {
            ClassSchedules = new HashSet<ClassSchedules>();
        }

        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<ClassSchedules> ClassSchedules { get; set; }
    }
}
