using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public string Role { set; get; }
    }
}
