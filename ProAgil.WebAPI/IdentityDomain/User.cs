using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.WebAPI.IdentityDomain
{
    public class User: IdentityUser<int>
    {
        public List<UserRole> UeserRoles { get; set; }
    }
}