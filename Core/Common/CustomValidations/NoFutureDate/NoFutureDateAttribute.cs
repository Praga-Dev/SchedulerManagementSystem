using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Common.CustomValidations.NoFutureDate
{
    /// <summary>
    /// Specifies that a data field value should not be a future date.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NoFutureDateAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return name + " should not be a future date";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // if value is null or not valid tryParse will return DateTime.MinValue,
            // As we are handling the min & max comparison in condition
            // so, even it is MinValue it will return False
            if (!DateTime.TryParse(Convert.ToString(value), out DateTime currDateVal))
                return new ValidationResult(ErrorMessage = "Date is not in Valid Format");

            if (currDateVal <= DateTime.Now)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage = "Future Date is not Allowed");
        }
    }
}
