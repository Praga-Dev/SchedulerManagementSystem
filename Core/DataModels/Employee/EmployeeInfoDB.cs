﻿using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.DataModels.Employee
{
    public class EmployeeInfoDB : BaseEmployeeInfoDB
    {

        #region Employee Information IAD

        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public Guid BudgetDepartmentId { get; set; }
        public string BudgetDepartmentName { get; set; }

        

        //// TODO Need type 
        //public string ChargeCode { get; set; }
        
        public Guid TitleId { get; set; }
        public string TitleName { get; set; }

        // TODO Does Title and Employee Role same
        public Guid EmployeeRoleId { get; set; }
        public string EmployeeRoleName { get; set; }

        public int WorkHoursPerWeek { get; set; }

        // Employee
        public Guid DirectReportId { get; set; }
        public string DirectReportFirstName { get; set; }
        public string DirectReportMiddleName { get; set; }
        public string DirectReportLastName { get; set; }

        public string LoginID { get; set; }
        public DateTime IAD_StartDate { get; set; }

        public Guid HireTypeId { get; set; }
        public string HireTypeName { get; set; }

        // TODO create lookup class if needed
        public bool TimeSheetExempt { get; set; }

        // TODO create lookup class if needed
        public bool Allocation { get; set; }

        #endregion

        #region Application Security Information

        public Guid ATS_Admin_LevelID { get; set; }
        public string ATS_Admin_Level { get; set; }


        public Guid ATS_Security_LevelID { get; set; }
        public string ATS_Security_Level { get; set; }


        public Guid BPA_Management_LevelID { get; set; }
        public string BPA_Management_Level { get; set; }

        // Access Level Type Info DB
        public Guid RITA_LevelID { get; set; }
        public string RITA_Level { get; set; }

        // Access Level Type Info DB
        public Guid BPA_LevelID { get; set; }
        public string BPA_Level { get; set; }

        #endregion

        #region Employee Information AIG

        public string AIG_EmployeeNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime AIG_StartDate { get; set; }
        public DateTime AIG_TerminationDate { get; set; }

        public Guid TerminationTypeId { get; set; }
        public string TerminationType { get; set; }

        public DateTime LOA_Date { get; set; }
        public DateTime ReInstateDate { get; set; }

        #endregion
    }
}
