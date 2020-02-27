using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Students
    {
        public Students()
        {
            Attendances = new HashSet<Attendances>();
            FinalScores = new HashSet<FinalScores>();
            Takes = new HashSet<Takes>();
        }

        public int StudentId { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
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
        public virtual ICollection<FinalScores> FinalScores { get; set; }
        public virtual ICollection<Takes> Takes { get; set; }
    }
}
