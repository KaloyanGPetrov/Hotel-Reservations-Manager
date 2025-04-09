using Microsoft.AspNetCore.Identity;

namespace Hotel_Reservations_Manager.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public string EGN { get; set; }

        public DateOnly DateOfHire { get; set; }

        public bool IsActive { get; set; }

        public DateOnly DateOfLetingGo { get; set; }
    }
}
