using System;
using System.ComponentModel.DataAnnotations;
using TOT.Entities.IdentityEntities;

namespace TOT.Dto.TimeOffRequests
{
    public class TimeOffRequestApprovalDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        [Display(Name = "Solved")]
        public DateTime? SolvedDate { get; set; }

        public string Reason { get; set; }     

        public TimeOffRequestApprovalStatusesDTO Status { get; set; }

        [Display(Name = "Request")]
        public TimeOffRequestDTO TimeOffRequest { get; set; }

        public int TimeOffRequestId { get; set; }
    }

    public enum TypeOfApprovalsList : int
    {
        RequestedApprovals = 1,
        RefusedApprovals = 2,
        AllMyApprovals = 3,
        AllApprovals = 4
    }
}
