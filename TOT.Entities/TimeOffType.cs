namespace TOT.Entities.TimeOffRequests
{
    public class TimeOffType
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public enum TimeOffTypeEnum : int
    {
        PaidHoliday = 1,
        UnpaidLeave = 2,
        StudyHoliday = 3,
        SickLeave = 4
    }
}
