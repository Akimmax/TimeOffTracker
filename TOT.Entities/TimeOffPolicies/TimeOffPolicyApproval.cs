using TOT.Entities.IdentityEntities;

namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicyApproval
    {
        public int Id { get; set; }
        public int Amount { get; set; }//amount of checkers
        public User User { get; set; }//manager who should approve this type of time off
        public EmployeePosition EmployeePosition { get; set; }
        public int EmployeePositionId { get; set; }

        public int EmployeePositionTimeOffPolicyId { get; set; }
    }
}
