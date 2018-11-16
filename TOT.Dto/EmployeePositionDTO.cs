using System.ComponentModel.DataAnnotations;

namespace TOT.Dto
{
    public class EmployeePositionDTO
    {
        public int Id { get; set; }
        [Display(Name ="Employee Position")]
        public string Title { get; set; }
    }
}
