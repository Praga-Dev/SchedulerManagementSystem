using Microsoft.AspNetCore.Mvc;
using SchedulerManagementSystem.Models.Lookups;
using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Models.Employee
{

    public class BaseEmployeeInfo
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        [StringLength(20, MinimumLength = 3)]
        public string MiddleName { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
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
