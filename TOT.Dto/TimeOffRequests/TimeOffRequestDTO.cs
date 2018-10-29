using System;
using System.Collections.Generic;
using System.Text;

namespace TOT.Dto.TimeOffRequests
{
    public class TimeOffRequestDTO
    {
        public TimeOffRequestDTO()
        {
            Approvals = new List<TimeOffRequestApprovalDTO>();
        }
        public int Id { get; set; }
        public string User { get; set; }
        public TimeOffTypeDTO Type { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime? EndsOn { get; set; }
        public string Note { get; set; }

        public ICollection<TimeOffRequestApprovalDTO> Approvals { get; set; }

    }
}
