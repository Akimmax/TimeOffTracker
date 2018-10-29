﻿using System;
using System.Collections.Generic;
using TOT.Entities.TimeOffRequests;

namespace TOT.Entities.TimeOffPolicies
{
    public class EmployeePositionTimeOffPolicy
    {
        public EmployeePositionTimeOffPolicy()
        {
            Approvals = new List<TimeOffPolicyApproval>();
        }
        public int Id { get; set; }
        public TimeOffType Type { get; set; }
        public TimeOffPolicy Policy { get; set; }
        public EmployeePosition Position { get; set; }
        public ICollection<TimeOffPolicyApproval> Approvals { get; set; }
    }
}