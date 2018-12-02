using System.ComponentModel.DataAnnotations;

namespace TOT.Dto.TimeOffPolicies
{
    public class EmployeePositionDTO
    {
        public int Id { get; set; }
        [Display(Name ="Employee Position")]
        public string Title { get; set; }
    }
}
