using System;
using System.Collections.Generic;
using TOT.Entities.TimeOffRequests;

namespace TOT.Entities.TimeOffPolicies
{
    public class EmployeePositionTimeOffPolicy
    {
        public EmployeePositionTimeOffPolicy()
        {
            Approvals = new List<TimeOffPolicyApprover>();
        }
        public int Id { get; set; }
        public TimeOffType Type { get; set; }
        public int TypeId { get; set; }
        public TimeOffPolicy Policy { get; set; }
        public int PolicyId { get; set; }
        public bool IsActive {get;set;}
        public EmployeePositionTimeOffPolicy NextPolicy { get; set; }
        public EmployeePosition Position { get; set; }
        public ICollection<TimeOffPolicyApprover> Approvals { get; set; }
    }
}
