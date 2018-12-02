using System;
using System.Collections.Generic;

namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicyApprover
    {
        public int Id { get; set; }
        public int Amount { get; set; }//amount of checkers
        public EmployeePosition EmployeePosition { get; set; }
        public int EmployeePositionId { get; set; }

        public int EmployeePositionTimeOffPolicyId { get; set; }
    }

    public class TimeOffPolicyApproverComparer : IEqualityComparer<TimeOffPolicyApprover>
    {
        public bool Equals(TimeOffPolicyApprover x, TimeOffPolicyApprover y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            return x != null && y != null && x.Amount == y.Amount && x.EmployeePositionId == y.EmployeePositionId &&
                x.EmployeePositionTimeOffPolicyId == y.EmployeePositionTimeOffPolicyId;
        }

        public int GetHashCode(TimeOffPolicyApprover obj)
        {
            return obj.GetHashCode();
        }
    }
}
