using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TOT.Dto.CustomValidationAttributes;
using TOT.Entities.IdentityEntities;

namespace TOT.Dto.TimeOffRequests
{
    public class TimeOffRequestDTO
    {
        public TimeOffRequestDTO()
        {
            Approvals = new List<TimeOffRequestApprovalDTO>();
        }
        public int Id { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }

        public TimeOffTypeDTO Type { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = " Request type is required")]
        public int? TypeId { get; set; }

        [Display(Name = "Start")]
        [Required(ErrorMessage = "The value is invalid.")]
        [DateLessThan("EndsOn", ErrorMessage = "Start date must be earlier than End date")]
        [DataType(DataType.Date)]
        public DateTime StartsAt { get; set; }

        [Display(Name = "End")]
        [Required(ErrorMessage = "The value is invalid.")]
        [DataType(DataType.Date)]
        public DateTime? EndsOn { get; set; }

        public string Note { get; set; }

        public ICollection<TimeOffRequestApprovalDTO> Approvals { get; set; }
        [Required(ErrorMessage = " Request approvals is required")]
        [RequestApprovalsWithoutRepeatAttribute(ErrorMessage = "Approvals mustn't repeat")]
        public ICollection<string> ApproversId { get; set; }

    }
}
