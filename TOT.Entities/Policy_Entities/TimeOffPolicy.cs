using System.Collections.Generic;
using TOT.Entities.Request_Entities;

namespace TOT.Entities.Policy_Entities
{
    public class TimeOffPolicy
    {
        public TimeOffPolicy()
        {
            AccrualSchedules = new List<AccrualSchedule>();
            TimeOffPolicyCheckers = new List<TimeOffPolicyCheckers>();
        }
        public int Id { get; set; }
        public TimeOffType TimeOffType { get; set; }
        public Positions Position { get; set; }
        public ICollection<TimeOffPolicyCheckers> TimeOffPolicyCheckers { get; set; }
        public ICollection<AccrualSchedule> AccrualSchedules { get; set; }
    }
}
