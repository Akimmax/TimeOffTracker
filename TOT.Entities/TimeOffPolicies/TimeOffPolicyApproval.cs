using TOT.Entities.IdentityEntities;

namespace TOT.Entities.TimeOffPolicies
{
    public class TimeOffPolicyApproval
    {
        public int Id { get; set; }
        public int Amount { get; set; }//amount of checkers
        public EmployeePosition EmployeePosition { get; set; }
        public int EmployeePositionId { get; set; }

        public int EmployeePositionTimeOffPolicyId { get; set; }
    }
}
