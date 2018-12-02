using System;
using System.Collections.Generic;
using System.Text;
using TOT.Dto.TimeOffRequests;
using TOT.Entities;

namespace TOT.Dto.Identity.Models
{
    public class TimeOffRequestFilterModel
    {
        public int Id { get; set; }
        public TimeOffTypeDTO Type { get; set; }
        public int? TypeId { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsOn { get; set; }
        public string Note { get; set; }
        public int? RequestStatuses { get; set; }

    }
}
