using System.ComponentModel.DataAnnotations;

namespace TOT.Dto
{
    public class TimeOffPolicyApproverDTO
    {
        public int Id { get; set; }
        [Display(Name ="Amount of Approvals")]
        public int Amount { get; set; }
        [Display(Name = "Approvals position")]
        public EmployeePositionDTO EmployeePosition { get; set; }
        //public int EmployeePositionId { get; set; }

        public int EmployeePositionTimeOffPolicyId { get; set; }
    }
}
