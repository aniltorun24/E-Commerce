using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace e_CommerceApi.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
