using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TOT.Dto
{
    public class EmployeePositionTimeOffPolicyDTO
    {
        public EmployeePositionTimeOffPolicyDTO()
        {
            Approvals = new List<TimeOffPolicyApproverDTO>();
        }
        public int Id { get; set; }
        [Display(Name = "Vacation Type")]
        public TimeOffTypeDTO Type { get; set; }
        //public int TypeId { get; set; }
        [Display(Name = "Vacation Policy")]
        public TimeOffPolicyDTO Policy { get; set; }
        //public int PolicyId { get; set; }
        //public bool IsActive { get; set; }
        //public EmployeePositionTimeOffPolicyDTO NextPolicy { get; set; }
        [Display(Name = "Employee Position")]
        public EmployeePositionDTO Position { get; set; }
        [Display(Name = "Requst Approvals")]
        public IList<TimeOffPolicyApproverDTO> Approvals { get; set; }
    }
}
