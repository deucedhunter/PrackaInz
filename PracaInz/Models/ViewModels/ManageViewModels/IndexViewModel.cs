using System.ComponentModel.DataAnnotations;

namespace PracaInz.Models.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
