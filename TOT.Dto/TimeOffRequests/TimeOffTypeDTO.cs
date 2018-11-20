using System.ComponentModel.DataAnnotations;

namespace TOT.Dto.TimeOffRequests
{
    public class TimeOffTypeDTO
    {
        public int Id { get; set; }
        [Display(Name ="Vacation Type")]
        public string Title { get; set; }
    }
}
