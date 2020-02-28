using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class Semesters
    {
        public Semesters()
        {
            FinalScores = new HashSet<FinalScores>();
            Takes = new HashSet<Takes>();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public int SemesterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<FinalScores> FinalScores { get; set; }
        [JsonIgnore]
        public virtual ICollection<Takes> Takes { get; set; }
    }
}
