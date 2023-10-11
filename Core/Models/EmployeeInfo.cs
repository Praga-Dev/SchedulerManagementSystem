using SchedulerManagementSystem.Common.CustomValidations.NoFutureDate;
using SchedulerManagementSystem.Models.Employee;
using SchedulerManagementSystem.Models.Lookups;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Models
{
    public class EmployeeInfo : BaseEmployeeInfo
    {

        [Required]
        public TitleInfo TitleInfo { get; set; }

        [Required]
        public EmployeeRoleInfo EmployeeRoleInfo { get; set; }

        [Required(ErrorMessage ="Work Hours Per Week is required")]
        [Range(0, 50)]
        [Display(Name = "Work Hours Per Week")]
        public int WorkHoursPerWeek { get; set; }

        [Required]
        public DepartmentInfo DepartmentInfo { get; set; }
        
        [Required]
        public DepartmentInfo BudgetDepartmentInfo { get; set; }

        [Required]
        public BaseEmployeeInfo DirectReportInfo { get; set; }

        [Required(ErrorMessage = "Login ID is required")]
        [Range(0, 50)]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }
        
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="IAD Start Date is required")]
        [Display(Name = "IAD Start Date")]
        [NoFutureDate(ErrorMessage = "IAD Start Date should not be future Date")] // TODO Check with Joshua
        public DateTime IAD_StartDate { get; set; }

        [Required]
        public HireTypeInfo HireTypeInfo { get; set; }

        // TODO create lookup class if needed
        [Required(ErrorMessage = "Timesheet Exempt is required")]
        [Display(Name = "Timesheet Exempt")]
        public bool TimeSheetExempt { get; set; }

        // TODO create lookup class if needed
        [Required(ErrorMessage = "Allocation is required")]
        [Display(Name = "Allocation")]
        public bool Allocation { get; set; }


        #region Application Security Information

        [Required]
        public ATS_AdminLevelTypeInfo ATS_AdminLevelTypeInfo { get; set; }

        [Required]
        public ATS_SecurityLevelTypeInfo ATS_SecurityLevelTypeInfo { get; set; }
        
        public BPA_ManagementLevelTypeInfo BPA_ManagementLevelTypeInfo { get; set; }
        
        [Required]
        public AccessLevelTypeInfo RITA_LevelInfo { get; set; }
        
        [Required]
        public AccessLevelTypeInfo BPA_LevelInfo { get; set; }


        #endregion

        #region Employee Information AIG


        [Required(ErrorMessage = "AIG Employee Number is required")]
        [StringLength(15, MinimumLength =0, ErrorMessage = "AIG Employee Number length should be 5 to 15 characters")]
        [Display(Name = "AIG Employee Number")]
        public string AIG_EmployeeNumber { get; set; }
        
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage ="Phone Number should be valid")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Email Address should be valid")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "AIG Start Date is required")]
        [Display(Name = "AIG Start Date")]
        [NoFutureDate(ErrorMessage = "AIG Start Date should not be future Date")] // TODO Check with Joshua
        public DateTime AIG_StartDate { get; set; }
        
        [NoFutureDate(ErrorMessage = "AIG Termination Date should not be past Date")] // TODO Check with Joshua
        public DateTime AIG_TerminationDate { get; set; }

        public TerminationTypeInfo TerminationTypeInfo { get; set; }

        public DateTime LOA_Date { get; set; }
        public DateTime ReInstateDate { get; set; }

        #endregion
    }
}
