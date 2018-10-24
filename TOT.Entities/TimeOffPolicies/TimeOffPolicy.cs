using TOT.Entities.TimeOffRequests;

namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOffType Type { get; set; }
        public int TimeOffDaysPerYear { get; set; }
    }
}
