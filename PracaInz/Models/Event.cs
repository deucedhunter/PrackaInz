using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models
{
    public class Event
    {
        public int EventID { get; set; }

        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime Time { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        public int AuthorID { get; set; }
        public Person Author { get; set; }

        public IEnumerable<EventClass> EventClass { get; set; }
    }
}
