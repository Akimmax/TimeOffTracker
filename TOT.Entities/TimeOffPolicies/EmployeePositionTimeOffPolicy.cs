using System;
using System.Collections.Generic;

namespace TOT.Entities.TimeOffPolicies
{
    public class EmployeePositionTimeOffPolicy
    {
        public EmployeePositionTimeOffPolicy()
        {
            Approvals = new List<TimeOffPolicyApproval>();
        }
        public int Id { get; set; }
        public EmployeePosition EmployeePosition { get; set; }
        public TimeOffPolicy Policy { get; set; }
        public ICollection<TimeOffPolicyApproval> Approvals { get; set; }
        public bool IsDelited { get; set; }
        public DateTime DateOfChanging { get; set; }
    }
}
