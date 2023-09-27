using SchedulerManagementSystem.Models.Lookups;
using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Models
{
    public class EmployeeInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public DepartmentInfo DepartmentInfo { get; set; }
        
        [Required]
        public DepartmentInfo BudgetDepartmentInfo { get; set; }

        [Required]
        public LocationInfo LocationInfo { get; set; }

        //// TODO Need type 
        //[Required]
        //public string ChargeCode { get; set; }

        [Required]
        public TitleInfo TitleInfo { get; set; }
        
        [Required]
        public EmployeeRoleInfo EmployeeRoleInfo { get; set; }

        [Required]
        public int WorkHoursPerWeek { get; set; }

        [Required]
        public EmployeeInfo DirectReportInfo { get; set; }

        [Required]
        public string LoginID { get; set; }
        
        [Required]
        public DateTime IAD_StartDate { get; set; }

        [Required]
        public HireTypeInfo HireTypeInfo { get; set; }

        // TODO create lookup class if needed
        [Required]
        public bool TimeSheetExempt { get; set; }

        // TODO create lookup class if needed
        [Required]
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

        
        [Required]
        public string AIG_EmployeeNumber { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public DateTime AIG_StartDate { get; set; }
        public DateTime AIG_TerminationDate { get; set; }

        public TerminationTypeInfo TerminationTypeInfo { get; set; }

        public DateTime LOA_Date { get; set; }
        public DateTime ReInstateDate { get; set; }

        #endregion


        #region Custom Fields

        public GradeInfo GradeInfo { get; set; }

        public int TotalHours { get; set; }
        
        #endregion
    }
}
