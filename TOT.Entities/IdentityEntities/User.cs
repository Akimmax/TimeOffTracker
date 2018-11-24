using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TOT.Entities.IdentityEntities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public int PositionId { get; set; }
        public EmployeePosition Position { get; set; }
        public DateTime HireDate { get; set; }
    }
}
