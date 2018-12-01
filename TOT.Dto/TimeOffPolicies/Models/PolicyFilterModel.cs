using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TOT.Entities;
using TOT.Entities.TimeOffPolicies;
using TOT.Entities.TimeOffRequests;

namespace TOT.Dto.TimeOffPolicies.Models
{
    public class PolicyFilterModel
    {
        [Display(Name="Time of Type")]
        public TimeOffType Type { get; set; }
        [Display(Name = "Employee Position")]
        public EmployeePosition Position { get; set; }
        [Display(Name = "Name of Policy")]
        public string Name { get; set; }
        [Display(Name = "Delay")]
        public int? DelayBeforeAvailable { get; set; }
        [Display(Name = "Should search by delay?")]
        public bool SearchByDelay { get; set; }
        [Display(Name = "Days per year")]
        public int? TimeOffDaysPerYear { get; set; }
        [Display(Name = "Is active")]
        public bool? IsActive { get; set; }
        [Display(Name = "Approvers Positions")]
        public EmployeePosition ApproverPositions { get; set; }
    }
}
