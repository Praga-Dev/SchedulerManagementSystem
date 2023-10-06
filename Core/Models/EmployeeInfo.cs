using SchedulerManagementSystem.Models.Employee;
using SchedulerManagementSystem.Models.Lookups;
using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Models
{
    public class EmployeeInfo : BaseEmployeeInfo
    {

        [Required]
        public TitleInfo TitleInfo { get; set; }

        [Required]
        public EmployeeRoleInfo EmployeeRoleInfo { get; set; }

        [Required]
        [Range(0, 50)]
        public int WorkHoursPerWeek { get; set; }

        [Required]
        public DepartmentInfo DepartmentInfo { get; set; }
        
        [Required]
        public DepartmentInfo BudgetDepartmentInfo { get; set; }

        [Required]
        public BaseEmployeeInfo DirectReportInfo { get; set; }

        [Required]
        public string LoginID { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
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
    }
}
