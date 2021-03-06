﻿using System;
using System.Collections.Generic;
using TOT.Entities.TimeOffPolicies;
using TOT.Entities.IdentityEntities;

namespace TOT.Entities.TimeOffRequests
{
    public class TimeOffRequest
    {
        public TimeOffRequest()
        {
            Approvals = new List<TimeOffRequestApproval>();
        }
        public int Id { get; set; }
        public string UserId { get; set; } 
        public User User { get; set; } //employee who request time off
        public int TypeId { get; set; }
        public TimeOffType Type { get; set; }//Type of timeoff
        public DateTime StartsAt { get; set; }// date of starting timeoff
        public DateTime? EndsOn { get; set; }// date of timeoff ending
        public string Note { get; set; }// extra information
        public EmployeePositionTimeOffPolicy Policy { get; set; }
        public ICollection<TimeOffRequestApproval> Approvals { get; set; }
    }
}
