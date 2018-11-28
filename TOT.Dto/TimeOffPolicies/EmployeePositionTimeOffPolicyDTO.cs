using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TOT.Dto.TimeOffRequests;

namespace TOT.Dto.TimeOffPolicies
{
    public class EmployeePositionTimeOffPolicyDTO
    {
        public EmployeePositionTimeOffPolicyDTO()
        {
            Approvers = new List<TimeOffPolicyApproverDTO>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Vacation Type should be defined")]
        [Display(Name = "Vacation Type")]
        public TimeOffTypeDTO Type { get; set; }
        [Required(ErrorMessage = "Policy should be defined")]
        [Display(Name = "Vacation Policy")]
        public TimeOffPolicyDTO Policy { get; set; }
        [Display(Name = "Status Policy")]
        public bool IsActive { get; set; }
        public EmployeePositionTimeOffPolicyDTO NextPolicy { get; set; }
        [Required(ErrorMessage = "Employee Position should be defined")]
        [Display(Name = "Employee Position")]
        public EmployeePositionDTO Position { get; set; }
        [Required(ErrorMessage = "At least one approver should be defined")]
        [Display(Name = "Requst Approvals")]
        public IList<TimeOffPolicyApproverDTO> Approvers { get; set; }
    }
}
