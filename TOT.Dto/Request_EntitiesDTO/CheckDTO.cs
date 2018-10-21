using System;
using System.ComponentModel.DataAnnotations;

namespace TOT.Dto.Request_EntitiesDTO
{
    public class CheckDTO
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [Display(Name = "Solved Date")]
        public DateTime SolvedDate { get; set; }

        public string Reason { get; set; }

        public RequestStatusDTO Status { get; set; }

        public TimeOffRequestDTO TimeOffRequest { get; set; }
    }
}