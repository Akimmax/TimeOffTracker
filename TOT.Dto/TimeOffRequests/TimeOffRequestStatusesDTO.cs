namespace TOT.Dto.TimeOffRequests
{
    public class TimeOffRequestStatusesDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public enum RequestStatuses : int
    {
        InProcess = 1,
        Denied = 2,
        Accepted = 3
    }
}