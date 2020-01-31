using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.WebAPI.IdentityDomain
{
    public class Role: IdentityRole<int>
    {
        public List<UserRole> UeserRoles { get; set; }
    }
}