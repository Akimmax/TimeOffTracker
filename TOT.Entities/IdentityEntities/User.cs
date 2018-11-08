using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TOT.Entities.IdentityEntities
{
    public class User : IdentityUser
    {
        public int PositionId { get; set; }
        public EmployeePosition Position { get; set; }
    }
}
