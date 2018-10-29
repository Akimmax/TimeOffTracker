using System;

namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? DelayBeforeUse { get; set; }//delay before can take Vacation. if null none delay
        public int TimeOffDaysPerYear { get; set; }
    }
}
