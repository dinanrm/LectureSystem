using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class Semesters
    {
        public Semesters()
        {
            FinalScores = new HashSet<FinalScores>();
            Takes = new HashSet<Takes>();
        }

        public int SemesterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<FinalScores> FinalScores { get; set; }
        public virtual ICollection<Takes> Takes { get; set; }
    }
}
