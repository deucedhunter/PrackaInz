using System;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Presence
    {
        public int PresenceID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [DataType(DataType.Time)]
        public DateTime Godzina { get; set; }

        [Display(Name = "Obecność")]
        public bool IsPresent { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int? CourseID { get; set; }
        public virtual Course Course { get; set; }

    }
}
