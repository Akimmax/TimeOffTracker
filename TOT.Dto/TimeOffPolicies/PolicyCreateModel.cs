using System.ComponentModel.DataAnnotations;
using TOT.Dto.TimeOffRequests;

namespace TOT.Dto.TimeOffPolicies
{
    public class PolicyCreateModel
    {
        public const int MinProbation = 1;
        public const int MaxProbation = 12;

        public int Id { get; set; }
        [Required(ErrorMessage = "Vacation Type should be defined")]
        [Display(Name = "Vacation Type")]
        public TimeOffTypeDTO Type { get; set; }
        [Display(Name = "Status Policy")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Employee Position should be defined")]
        [Display(Name = "Employee Position")]
        public EmployeePositionDTO Position { get; set; }
        public int PolicyId { get; set; }
        [Display(Name = "Policy Name")]
        [Required(ErrorMessage = "Policy name should be defined")]
        public string Name { get; set; }
        [Display(Name = "Probation")]
        [Range(MinProbation, MaxProbation, ErrorMessage = "Value out of range")]
        public int? DelayBeforeAvailable { get; set; }
        [Display(Name = "Vacation days per year")]
        [Required(ErrorMessage = "Vacations days should be defined")]
        public int TimeOffDaysPerYear { get; set; }
        [Required(ErrorMessage = "At least one approver should be defined")]
        [Display(Name = "Requst Approvals")]
        public string Approvers { get; set; }
    }
}
