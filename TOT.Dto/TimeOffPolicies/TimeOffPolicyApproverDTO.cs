using System.ComponentModel.DataAnnotations;
using TOT.Dto.TimeOffPolicies;

namespace TOT.Dto.TimeOffPolicies
{
    public class TimeOffPolicyApproverDTO
    {
        public int Id { get; set; }
        [Display(Name ="Amount of Approvals")]
        public int Amount { get; set; }
        [Display(Name = "Approvals position")]
        public EmployeePositionDTO EmployeePosition { get; set; }

        public int EmployeePositionTimeOffPolicyId { get; set; }
    }
}
