using System.Collections.Generic;
using TOT.Entities.TimeOffRequests;

namespace TOT.Entities.TimeOffPolicies
{
    public class EmployeePositionTimeOffPolicy
    {
        public EmployeePositionTimeOffPolicy()
        {
            AccrualSchedules = new List<AccrualSchedule>();
            Approvals = new List<TimeOffPolicyCheckers>();
        }
        public int Id { get; set; }
        public EmployeePosition EmployeePosition { get; set; }
        public TimeOffPolicy Policy { get; set; }
        public ICollection<TimeOffPolicyApproval> Approvals { get; set; }
        public ICollection<AccrualSchedule> AccrualSchedules { get; set; }
    }
}
