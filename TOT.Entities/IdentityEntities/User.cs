using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TOT.Entities.IdentityEntities
{
    public class User : IdentityUser
    {
        public DateTime HireDate { get; set; }
    }
}
