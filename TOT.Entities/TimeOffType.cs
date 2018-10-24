namespace TOT.Entities.TimeOffRequests
{
    public class TimeOffType
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public enum TimeOffTypeEnum : int
    {
        PayedTimeOff = 1
    }
}
