using Microsoft.AspNetCore.Identity;

namespace BookStore.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public DateTime BirthDate { get; set; }
    }
}
