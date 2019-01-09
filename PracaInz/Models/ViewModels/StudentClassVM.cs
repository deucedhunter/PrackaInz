using System.Collections.Generic;

namespace PracaInz.Models.ViewModels
{
    public class StudentClassVM
    {
        public Student Student { get; set; }
        public IEnumerable<Class> Classes { get; set; }
    }
}
