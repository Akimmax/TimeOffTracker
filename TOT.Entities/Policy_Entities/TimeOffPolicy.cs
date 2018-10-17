using System.Collections.Generic;
using TOT.Entities.Request_Entities;

namespace TOT.Entities.Policy_Entities
{
    public class TimeOffPolicy
    {
        public TimeOffPolicy()
        {
            AccrualSchedules = new List<AccrualSchedule>();
        }
        public int Id { get; set; }
        public TimeOffType TimeOffType { get; set; }
        public Policy Policy { get; set; }
        public ICollection<AccrualSchedule> AccrualSchedules { get; set; }
    }
}
