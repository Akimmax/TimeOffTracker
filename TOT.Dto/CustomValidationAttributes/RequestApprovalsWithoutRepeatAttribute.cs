using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TOT.Dto.CustomValidationAttributes
{
    class RequestApprovalsWithoutRepeatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value == null)
            {
                return new ValidationResult(ErrorMessage);
            }

            ICollection<string> UserApproveIds = (ICollection<string>)value;

            foreach (var apr in UserApproveIds)
            {
                if (UserApproveIds.Count(a => a == apr) != 1)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}