namespace TOT.Entities.TimeOffRequests
{
    public class TimeOffType
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public enum TimeOffTypeEnum : int
    {
        PaidVacation = 1,
        UnpaidVacation = 2,
        StudyVacation = 3,
        SickVacation = 4
    }
}
