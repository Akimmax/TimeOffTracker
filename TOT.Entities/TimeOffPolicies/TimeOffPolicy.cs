using System;
using TOT.Entities.TimeOffRequests;

namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ResetDate { get; set; }// date of reset timeoff
        public int TimeOffDaysPerYear { get; set; }
    }
}
