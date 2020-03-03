using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class CourseScores
    {
        public CourseScores()
        {
            Status = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
        public int CourseScoreId { get; set; }
        public int? TakeId { get; set; }
        public int? ScoreId { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public virtual Scores Score { get; set; }
        [JsonIgnore]
        public virtual Takes Take { get; set; }
    }
}
