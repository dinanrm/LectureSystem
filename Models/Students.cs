using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LectureSystem.Models
{
    public partial class Students
    {
        public Students()
        {
            Attendances = new HashSet<Attendances>();
            FinalScores = new HashSet<FinalScores>();
            Takes = new HashSet<Takes>();

            UUID = Guid.NewGuid().ToString();
            Status = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public int StudentId { get; set; }
        public string UUID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Attendances> Attendances { get; set; }
        [JsonIgnore]
        public virtual ICollection<FinalScores> FinalScores { get; set; }
        [JsonIgnore]
        public virtual ICollection<Takes> Takes { get; set; }
    }
}
