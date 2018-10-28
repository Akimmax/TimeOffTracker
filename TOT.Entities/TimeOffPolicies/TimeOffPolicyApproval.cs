namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicyApproval
    {
        public int Id { get; set; }
        public int Amount { get; set; }//amount of checkers
        public string UserId { get; set; }//manager who should approve this type of time off
        public EmployeePosition Position { get; set; }
    }
}
