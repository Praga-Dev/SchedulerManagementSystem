using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SchedulerManagementSystem.Models.Lookups;
using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Models.Employee
{

    public class BaseEmployeeInfo
    {
        [ValidateNever]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        [StringLength(20, MinimumLength = 1)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //// TODO Need type 
        //[Required]
        //public string ChargeCode { get; set; }

        [Required]
        public LocationInfo LocationInfo { get; set; }
        
        [Required]
        public GradeInfo GradeInfo { get; set; }


        #region Custom Fields

        // TODO if possible move to CalendarEmployeeInfo
        public int TotalHours { get; set; }

        #endregion

    }
}
