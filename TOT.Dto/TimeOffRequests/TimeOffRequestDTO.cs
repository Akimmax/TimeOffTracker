using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TOT.Dto.CustomValidationAttributes;

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

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        [Display(Name = "Start")]
        [DateLessThan("EndsOn", ErrorMessage = "Start date must be earlier than End date")]
        public DateTime StartsAt { get; set; }

        [Display(Name = "End")]
        public DateTime? EndsOn { get; set; }

        public string Note { get; set; }

        public ICollection<TimeOffRequestApprovalDTO> Approvals { get; set; }
        public ICollection<string> UsersApproveRequestId { get; set; }

    }
}
