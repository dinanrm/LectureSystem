using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class FinalScores
    {
        public FinalScores()
        {
            Status = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
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
