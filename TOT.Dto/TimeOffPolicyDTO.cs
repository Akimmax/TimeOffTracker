using System.ComponentModel.DataAnnotations;

namespace TOT.Dto
{
    public class TimeOffPolicyDTO
    {
        public const int MinProbation = 1;
        public const int MaxProbation = 12;

        public int Id { get; set; }
        [Display(Name = "Policy Name")]
        public string Name { get; set; }
        [Display(Name = "Probation")]
        [Range(MinProbation, MaxProbation, ErrorMessage = "Value out of range")]
        public int? DelayBeforeAvailable { get; set; }
        [Display(Name = "Vacation days per year")]
        [Required(ErrorMessage = "Vacations days should be defined")]
        public int TimeOffDaysPerYear { get; set; }
    }
}
