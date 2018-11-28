using System;
using System.Collections.Generic;
using System.Text;
using TOT.Entities;

namespace TOT.Dto.Identity.Models
{
    public class UserFilterModel
    {
        public string Id { get; set; }
        public bool Fired { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public ICollection<int> Position { get; set; }
        public DateTime fromHireDate { get; set; }
        public DateTime toHireDate { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
