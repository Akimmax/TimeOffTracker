using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TOT.Entities;

namespace TOT.Dto.Identity
{
    public class UserDTO
    {
        public UserDTO()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Status")]
        public bool Fired { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Employee position")]
        public EmployeePosition Position { get; set; }
        [Required]
        [Display(Name = "Hire date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [Required]
        public ICollection<string> Roles { get; set; }
    }
}
