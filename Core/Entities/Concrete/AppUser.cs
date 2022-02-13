using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }

        public string Picture { get; set; }

        public DateTime? BirthDate { get; set; }
 

    }
}
