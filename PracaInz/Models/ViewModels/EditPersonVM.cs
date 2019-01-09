namespace PracaInz.Models.ViewModels
{
    public class EditPersonVM
    {
        public Person Person { get; set; }

        public Role? Role { get; set; }
    }

    public enum Role
    {
        Student,
        Teacher,
        Other
    }
}
