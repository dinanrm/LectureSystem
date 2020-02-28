using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class Scores
    {
        public Scores()
        {
            CourseScores = new HashSet<CourseScores>();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public int ScoreId { get; set; }
        public double? MinScore { get; set; }
        public string Alphabet { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<CourseScores> CourseScores { get; set; }
    }
}
