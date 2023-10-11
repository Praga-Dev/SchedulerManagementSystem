using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Common.CustomValidations
{
    /// <summary>
    /// Specifies that a data field value should not be a past date.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NoPastDateAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        { 
            return name + " should not be a Past date";
        }

        public override bool IsValid(object value)
        {
            // if value is null or not valid tryParse will return DateTime.MinValue,
            // As we are handling the min & max comparison in condition
            // so, even it is MinValue it will return False
            if (!DateTime.TryParse(Convert.ToString(value), out DateTime currDateVal))
                return false;

            return currDateVal >= DateTime.Now;
        }
    }
}
