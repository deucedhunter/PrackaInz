using System.Collections.Generic;

namespace PracaInz.Models.ViewModels
{
    public class NewEventVM
    {
        public Event Event { get; set; }
        public IEnumerable<Event> EventList { get; set; }
        public IEnumerable<Class> Classes { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
