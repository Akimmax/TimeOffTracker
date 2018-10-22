namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicy
    {
        public int Id { get; set; }
        public double TimeAmount { get; set; }//time counting back from hired date
        public TimeMeasures TimeMeasure { get; set; }//Measure (days,month,years) of time counting back from hired date
        public int TimeOffDaysPerYear { get; set; }
    }
}
