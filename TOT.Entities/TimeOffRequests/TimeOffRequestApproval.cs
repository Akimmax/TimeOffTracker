using System;
using TOT.Entities.IdentityEntities;

namespace TOT.Entities.TimeOffRequests
{
    public class TimeOffRequestApproval
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }//employee who should accept request
        public DateTime? SolvedDate { get; set; }// date of decision
        public string Reason { get; set; }// reason of decision
        public TimeOffRequestApprovalStatuses Status { get; set; }// status of request {requsted, in_progres, denied, accepted}
        public TimeOffRequest TimeOffRequest { get; set; }
        public int TimeOffRequestId { get; set; }
    }
}
