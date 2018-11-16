using System.ComponentModel.DataAnnotations;

namespace TOT.Dto
{
    public class TimeOffTypeDTO
    {
        public int Id { get; set; }
        [Display(Name ="Vacation Type")]
        public string Title { get; set; }
    }
}
