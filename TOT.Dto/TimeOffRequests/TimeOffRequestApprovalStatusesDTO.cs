namespace TOT.Dto.TimeOffRequests
{
    public class TimeOffRequestApprovalStatusesDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public enum ApprovalStatuses : int
    {
        Requested = 1,
        InProgres = 2,
        Denied = 3,
        Accepted = 4
    }
}