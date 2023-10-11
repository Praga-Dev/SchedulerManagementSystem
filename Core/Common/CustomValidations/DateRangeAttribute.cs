using SchedulerManagementSystem.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace SchedulerManagementSystem.Common.CustomValidations
{

    ///<summary>
    ///    DateRange will return True if the value is InBound to given min & max dates, else False
    ///</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class DateRangeAttribute : ValidationAttribute
    {
        private DateTime? _MinDate { get; set; }
        private DateTime? _MaxDate { get; set; }

        public DateRangeAttribute(string dateStr, DateRangeSpecifierEnum dateRangeSpecifierEnum)
        {
            if (DateTime.TryParse(dateStr, out DateTime date))
            {
                if (dateRangeSpecifierEnum == DateRangeSpecifierEnum.MinDate)
                {
                    _MinDate = date;
                }
                else
                {
                    _MaxDate = date;
                }
            }

            _MinDate ??= DateTime.MinValue;
            _MaxDate ??= DateTime.MaxValue;
        }

        public DateRangeAttribute(string minDateStr, string maxDateStr)
        {
            if (DateTime.TryParse(minDateStr, out DateTime minDate))
            {
                _MinDate = minDate;
            }

            if (DateTime.TryParse(maxDateStr, out DateTime maxDate))
            {
                _MaxDate = maxDate;
            }
        }

        public override bool IsValid(object value)
        {
            DateTime currDateVal;

            // if value is null or not valid tryParse will return DateTime.MinValue,
            // As we are handling the min & max comparison in condition
            // so, even it is MinValue it will return False
            if (!DateTime.TryParse(Convert.ToString(value), out currDateVal))
                return false;

            return currDateVal >= _MinDate && currDateVal <= _MaxDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }
    }
}
