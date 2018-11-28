using System;
using System.ComponentModel.DataAnnotations;

namespace TOT.Dto.CustomValidationAttributes
{
    public class DateLessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateLessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var propertyValue = property.GetValue(validationContext.ObjectInstance);

            if (propertyValue == null)
                return new ValidationResult(ErrorMessage);

            var comparisonValue = (DateTime)propertyValue;

            if (currentValue > comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
