using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Lecturers
    {
        public Lecturers()
        {
            Attendances = new HashSet<Attendances>();
            Fields = new HashSet<Fields>();
            Teaches = new HashSet<Teaches>();
        }

        public int LecturerId { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<Attendances> Attendances { get; set; }
        public virtual ICollection<Fields> Fields { get; set; }
        public virtual ICollection<Teaches> Teaches { get; set; }
    }
}
