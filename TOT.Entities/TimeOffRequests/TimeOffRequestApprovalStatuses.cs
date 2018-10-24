namespace TOT.Entities.TimeOffRequests
{
    public class TimeOffRequestApprovalStatuses
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public enum TimeOffRequestApprovalStatusesEnum : int
    {
        Requested = 1,
        InProgres = 2,
        Denied = 3,
        Accepted = 4
    }
}
