using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class FinalScores
    {
        public int FinalScoreId { get; set; }
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public double? GradePoint { get; set; }
        public double? GradePointAverage { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Semesters Semester { get; set; }
        public virtual Students Student { get; set; }
    }
}
