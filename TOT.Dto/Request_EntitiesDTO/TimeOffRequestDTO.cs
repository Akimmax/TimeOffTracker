using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TOT.Dto.Request_EntitiesDTO
{
    public class TimeOffRequestDTO
    {
        public TimeOffRequestDTO()
        {
            Checks = new List<CheckDTO>();
        }

        public int Id { get; set; }

        public string User { get; set; }

        [Display(Name = "Type")]
        public TimeOffTypeDTO TimeOffType { get; set; }

        public DateTime StartTimeOffDate { get; set; }

        public DateTime EndTimeOffDate { get; set; }

        public string Note { get; set; }

        [Display(Name = "Time Off")]
        public double AmountDaysOff { get; set; }

        public string Approved { get; set; }

        public ICollection<CheckDTO> Checks { get; set; }
    }
}
