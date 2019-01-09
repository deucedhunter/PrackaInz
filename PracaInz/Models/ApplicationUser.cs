using Microsoft.AspNetCore.Identity;

namespace PracaInz.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public int OwnerID { get; set; }

        public Person Person { get; set; }

    }
}
