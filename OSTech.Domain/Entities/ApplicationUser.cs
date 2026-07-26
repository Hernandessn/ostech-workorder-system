using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set;  }
        public DateTime RefreshTokenExpiryToken { get; set; }
    }
}
