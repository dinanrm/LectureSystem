using System;
using System.Collections.Generic;

namespace LectureSystem.Models
{
    public partial class CourseScores
    {
        public int CourseScoreId { get; set; }
        public int? TakeId { get; set; }
        public int? ScoreId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Scores Score { get; set; }
        public virtual Takes Take { get; set; }
    }
}
