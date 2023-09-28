using SchedulerManagementSystem.DataModels.Employee;
using SchedulerManagementSystem.Models;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class EmployeeCommonMapper
    {
        public static EmployeeInfo GetEmployeeInfo(EmployeeInfoDB employeeInfoDB)
        {
            if (employeeInfoDB == null)
            {
                return new EmployeeInfo();
            }

            return new EmployeeInfo()
            {
                Id = employeeInfoDB.Id,
                FirstName = employeeInfoDB.FirstName,
                MiddleName = employeeInfoDB.MiddleName,
                LastName = employeeInfoDB.LastName,
                WorkHoursPerWeek = employeeInfoDB.WorkHoursPerWeek,
                LoginID = employeeInfoDB.LoginID,
                IAD_StartDate = employeeInfoDB.IAD_StartDate,
                TimeSheetExempt = employeeInfoDB.TimeSheetExempt,
                Allocation = employeeInfoDB.Allocation,
                AIG_EmployeeNumber = employeeInfoDB.AIG_EmployeeNumber,
                PhoneNumber = employeeInfoDB.PhoneNumber,
                Email = employeeInfoDB.Email,
                AIG_StartDate = employeeInfoDB.AIG_StartDate,
                AIG_TerminationDate = employeeInfoDB.AIG_TerminationDate,
                LOA_Date = employeeInfoDB.LOA_Date,
                ReInstateDate = employeeInfoDB.ReInstateDate,
                TotalHours = employeeInfoDB.TotalHours,
                DepartmentInfo = new()
                {
                    Id = employeeInfoDB.DepartmentId,
                    Name = employeeInfoDB.DepartmentName,
                },
                BudgetDepartmentInfo = new()
                {
                    Id = employeeInfoDB.BudgetDepartmentId,
                    Name = employeeInfoDB.BudgetDepartmentName,
                },
                LocationInfo = new()
                {
                    Id = employeeInfoDB.LocationId,
                    Name = employeeInfoDB.LocationName
                },
                GradeInfo = new()
                {
                    Id = employeeInfoDB.GradeId,
                    Name = employeeInfoDB.GradeName
                },
                TitleInfo = new()
                {
                    Id = employeeInfoDB.TitleId,
                    Name = employeeInfoDB.TitleName
                },
                EmployeeRoleInfo = new()
                {
                    Id = employeeInfoDB.EmployeeRoleId,
                    Name = employeeInfoDB.EmployeeRoleName
                },
                DirectReportInfo = new()
                {
                    Id = employeeInfoDB.DirectReportId,
                    // TODO handle this
                },
                HireTypeInfo = new()
                {
                    Id = employeeInfoDB.HireTypeId,
                    Name = employeeInfoDB.HireTypeName
                },
                ATS_AdminLevelTypeInfo = new()
                {
                    Id = employeeInfoDB.ATS_Admin_LevelID,
                    Name = employeeInfoDB.ATS_Admin_Level
                },
                ATS_SecurityLevelTypeInfo = new()
                {
                    Id = employeeInfoDB.ATS_Security_LevelID,
                    Name = employeeInfoDB.ATS_Security_Level
                },
                BPA_ManagementLevelTypeInfo = new()
                {
                    Id = employeeInfoDB.BPA_Management_LevelID,
                    Name = employeeInfoDB.BPA_Management_Level
                },
                RITA_LevelInfo = new()
                {
                    Id = employeeInfoDB.RITA_LevelID,
                    Name = employeeInfoDB.RITA_Level
                },
                BPA_LevelInfo = new()
                {
                    Id = employeeInfoDB.BPA_LevelID,
                    Name = employeeInfoDB.BPA_Level
                },
                TerminationTypeInfo = new()
                {
                    Id = employeeInfoDB.TerminationTypeId,
                    Name = employeeInfoDB.TerminationType
                }
            };
        }

        public static EmployeeInfoDB GetEmployeeInfoDB(EmployeeInfo employeeInfo)
        {
            if (employeeInfo == null)
            {
                return new EmployeeInfoDB();
            }

            var employeeInfoDB = new EmployeeInfoDB()
            {
                Id = employeeInfo.Id,
                FirstName = employeeInfo.FirstName,
                MiddleName = employeeInfo.MiddleName,
                LastName = employeeInfo.LastName,
                WorkHoursPerWeek = employeeInfo.WorkHoursPerWeek,
                LoginID = employeeInfo.LoginID,
                IAD_StartDate = employeeInfo.IAD_StartDate,
                TimeSheetExempt = employeeInfo.TimeSheetExempt,
                Allocation = employeeInfo.Allocation,
                AIG_EmployeeNumber = employeeInfo.AIG_EmployeeNumber,
                PhoneNumber = employeeInfo.PhoneNumber,
                Email = employeeInfo.Email,
                AIG_StartDate = employeeInfo.AIG_StartDate,
                AIG_TerminationDate = employeeInfo.AIG_TerminationDate,
                LOA_Date = employeeInfo.LOA_Date,
                ReInstateDate = employeeInfo.ReInstateDate,
                TotalHours = employeeInfo.TotalHours
                
            };

            if (employeeInfo.GradeInfo != null) 
            {
                employeeInfoDB.GradeId = employeeInfo.GradeInfo.Id;
                employeeInfoDB.GradeName = employeeInfo.GradeInfo.Name;
            }

            if (employeeInfo.LocationInfo != null)
            {
                employeeInfoDB.LocationId = employeeInfo.LocationInfo.Id;
                employeeInfoDB.LocationName = employeeInfo.LocationInfo.Name;
            }

            if (employeeInfo.TitleInfo != null)
            {
                employeeInfoDB.TitleId = employeeInfo.TitleInfo.Id;
                employeeInfoDB.TitleName = employeeInfo.TitleInfo.Name;
            }

            if (employeeInfo.EmployeeRoleInfo != null)
            {
                employeeInfoDB.EmployeeRoleId = employeeInfo.EmployeeRoleInfo.Id;
                employeeInfoDB.EmployeeRoleName = employeeInfo.EmployeeRoleInfo.Name;
            }

            if (employeeInfo.DepartmentInfo != null)
            {
                employeeInfoDB.DepartmentId = employeeInfo.DepartmentInfo.Id;
                employeeInfoDB.DepartmentName = employeeInfo.DepartmentInfo.Name;
            }

            if (employeeInfo.BudgetDepartmentInfo != null)
            {
                employeeInfoDB.BudgetDepartmentId = employeeInfo.BudgetDepartmentInfo.Id;
                employeeInfoDB.BudgetDepartmentName = employeeInfo.BudgetDepartmentInfo.Name;
            }

            if (employeeInfo.DirectReportInfo != null)
            {
                employeeInfoDB.DirectReportId = employeeInfo.DirectReportInfo.Id;
                
                // TODO list all the fields
                employeeInfoDB.DirectReportFirstName = employeeInfo.DirectReportInfo.FirstName;
                employeeInfoDB.DirectReportMiddleName = employeeInfo.DirectReportInfo.MiddleName;
                employeeInfoDB.DirectReportLastName = employeeInfo.DirectReportInfo.LastName;
            }

            if (employeeInfo.HireTypeInfo != null)
            {
                employeeInfoDB.HireTypeId = employeeInfo.HireTypeInfo.Id;
                employeeInfoDB.HireTypeName = employeeInfo.HireTypeInfo.Name;
            }

            if (employeeInfo.ATS_AdminLevelTypeInfo != null)
            {
                employeeInfoDB.ATS_Admin_LevelID = employeeInfo.ATS_AdminLevelTypeInfo.Id;
                employeeInfoDB.ATS_Admin_Level = employeeInfo.ATS_AdminLevelTypeInfo.Name;
            }

            if (employeeInfo.ATS_SecurityLevelTypeInfo != null)
            {
                employeeInfoDB.ATS_Security_LevelID = employeeInfo.ATS_SecurityLevelTypeInfo.Id;
                employeeInfoDB.ATS_Security_Level = employeeInfo.ATS_SecurityLevelTypeInfo.Name;
            }

            if (employeeInfo.BPA_ManagementLevelTypeInfo != null)
            {
                employeeInfoDB.BPA_Management_LevelID = employeeInfo.BPA_ManagementLevelTypeInfo.Id;
                employeeInfoDB.BPA_Management_Level = employeeInfo.BPA_ManagementLevelTypeInfo.Name;
            }

            if (employeeInfo.RITA_LevelInfo != null)
            {
                employeeInfoDB.RITA_LevelID = employeeInfo.RITA_LevelInfo.Id;
                employeeInfoDB.RITA_Level = employeeInfo.RITA_LevelInfo.Name;
            }

            if (employeeInfo.BPA_LevelInfo != null)
            {
                employeeInfoDB.BPA_LevelID = employeeInfo.BPA_LevelInfo.Id;
                employeeInfoDB.BPA_Level = employeeInfo.BPA_LevelInfo.Name;
            }

            if (employeeInfo.TerminationTypeInfo != null)
            {
                employeeInfoDB.TerminationTypeId = employeeInfo.TerminationTypeInfo.Id;
                employeeInfoDB.TerminationType = employeeInfo.TerminationTypeInfo.Name;
            }

            return employeeInfoDB;
        }

        public static List<EmployeeInfoDB> GetEmployeeInfoDBList(List<EmployeeInfo> employeeInfoList)
        {
            if (employeeInfoList == null)
            {
                return new List<EmployeeInfoDB>();
            }

            List<EmployeeInfoDB> response = new();

            response = employeeInfoList.ConvertAll(
                employeeInfo => new EmployeeInfoDB
                {
                    Id = employeeInfo.Id,
                    FirstName = employeeInfo.FirstName,
                    MiddleName = employeeInfo.MiddleName,
                    LastName = employeeInfo.LastName,
                    WorkHoursPerWeek = employeeInfo.WorkHoursPerWeek,
                    LoginID = employeeInfo.LoginID,
                    IAD_StartDate = employeeInfo.IAD_StartDate,
                    TimeSheetExempt = employeeInfo.TimeSheetExempt,
                    Allocation = employeeInfo.Allocation,
                    AIG_EmployeeNumber = employeeInfo.AIG_EmployeeNumber,
                    PhoneNumber = employeeInfo.PhoneNumber,
                    Email = employeeInfo.Email,
                    AIG_StartDate = employeeInfo.AIG_StartDate,
                    AIG_TerminationDate = employeeInfo.AIG_TerminationDate,
                    LOA_Date = employeeInfo.LOA_Date,
                    ReInstateDate = employeeInfo.ReInstateDate,
                    TotalHours = employeeInfo.TotalHours,

                    GradeId = (employeeInfo.GradeInfo != null ? employeeInfo.GradeInfo.Id : Guid.Empty),
                    GradeName = (employeeInfo.GradeInfo != null ? employeeInfo.GradeInfo.Name : string.Empty) ?? string.Empty,

                    LocationId = (employeeInfo.LocationInfo != null ? employeeInfo.LocationInfo.Id : Guid.Empty),
                    LocationName = (employeeInfo.LocationInfo != null ? employeeInfo.LocationInfo.Name : string.Empty) ?? string.Empty,
                    
                    
                    ATS_Admin_LevelID = (employeeInfo.ATS_AdminLevelTypeInfo != null ? employeeInfo.ATS_AdminLevelTypeInfo.Id : Guid.Empty),
                    ATS_Admin_Level = (employeeInfo.ATS_AdminLevelTypeInfo != null ? employeeInfo.ATS_AdminLevelTypeInfo.Name : string.Empty) ?? string.Empty,
                    
                    TerminationTypeId = (employeeInfo.TerminationTypeInfo != null ? employeeInfo.TerminationTypeInfo.Id : Guid.Empty),
                    TerminationType = (employeeInfo.TerminationTypeInfo != null ? employeeInfo.TerminationTypeInfo.Name : string.Empty) ?? string.Empty,
                    
                    ATS_Security_LevelID = (employeeInfo.ATS_SecurityLevelTypeInfo != null ? employeeInfo.ATS_SecurityLevelTypeInfo.Id : Guid.Empty),
                    ATS_Security_Level = (employeeInfo.ATS_SecurityLevelTypeInfo != null ? employeeInfo.ATS_SecurityLevelTypeInfo.Name : string.Empty) ?? string.Empty,
                    
                    BPA_LevelID = (employeeInfo.BPA_LevelInfo != null ? employeeInfo.BPA_LevelInfo.Id : Guid.Empty),
                    BPA_Level = (employeeInfo.BPA_LevelInfo != null ? employeeInfo.BPA_LevelInfo.Name : string.Empty) ?? string.Empty,
                    
                    BPA_Management_LevelID = (employeeInfo.BPA_ManagementLevelTypeInfo != null ? employeeInfo.BPA_ManagementLevelTypeInfo.Id : Guid.Empty),
                    BPA_Management_Level = (employeeInfo.BPA_ManagementLevelTypeInfo != null ? employeeInfo.BPA_ManagementLevelTypeInfo.Name : string.Empty) ?? string.Empty,
                    
                    BudgetDepartmentId = (employeeInfo.BudgetDepartmentInfo != null ? employeeInfo.BudgetDepartmentInfo.Id : Guid.Empty),
                    BudgetDepartmentName = (employeeInfo.BudgetDepartmentInfo != null ? employeeInfo.BudgetDepartmentInfo.Name : string.Empty) ?? string.Empty,
                    
                    DepartmentId = (employeeInfo.DepartmentInfo != null ? employeeInfo.DepartmentInfo.Id : Guid.Empty),
                    DepartmentName = (employeeInfo.DepartmentInfo != null ? employeeInfo.DepartmentInfo.Name : string.Empty) ?? string.Empty,
                    
                    DirectReportId = (employeeInfo.DirectReportInfo != null ? employeeInfo.DirectReportInfo.Id : Guid.Empty),
                    DirectReportFirstName = (employeeInfo.DirectReportInfo != null ? employeeInfo.DirectReportInfo.FirstName : string.Empty) ?? string.Empty,
                    DirectReportMiddleName = (employeeInfo.DirectReportInfo != null ? employeeInfo.DirectReportInfo.MiddleName : string.Empty) ?? string.Empty,
                    DirectReportLastName = (employeeInfo.DirectReportInfo != null ? employeeInfo.DirectReportInfo.LastName : string.Empty) ?? string.Empty,
                    
                    EmployeeRoleId = (employeeInfo.EmployeeRoleInfo != null ? employeeInfo.EmployeeRoleInfo.Id : Guid.Empty),
                    EmployeeRoleName = (employeeInfo.EmployeeRoleInfo != null ? employeeInfo.EmployeeRoleInfo.Name : string.Empty) ?? string.Empty,
                    
                    HireTypeId = (employeeInfo.HireTypeInfo != null ? employeeInfo.HireTypeInfo.Id : Guid.Empty),
                    HireTypeName = (employeeInfo.HireTypeInfo != null ? employeeInfo.HireTypeInfo.Name : string.Empty) ?? string.Empty,
                    
                    RITA_LevelID = (employeeInfo.RITA_LevelInfo != null ? employeeInfo.RITA_LevelInfo.Id : Guid.Empty),
                    RITA_Level = (employeeInfo.RITA_LevelInfo != null ? employeeInfo.RITA_LevelInfo.Name : string.Empty) ?? string.Empty,
                    
                    TitleId = (employeeInfo.TitleInfo != null ? employeeInfo.TitleInfo.Id : Guid.Empty),
                    TitleName = (employeeInfo.TitleInfo != null ? employeeInfo.TitleInfo.Name : string.Empty) ?? string.Empty,
                }); 

            return response;

        }

        public static List<EmployeeInfo> GetEmployeeInfoList(List<EmployeeInfoDB> employeeInfoDBList)
        {
            if (employeeInfoDBList == null)
            {
                return new List<EmployeeInfo>();
            }

            List<EmployeeInfo> response = employeeInfoDBList.ConvertAll(
                employeeInfoDB => new EmployeeInfo
                {
                    Id = employeeInfoDB.Id,
                    FirstName = employeeInfoDB.FirstName,
                    MiddleName = employeeInfoDB.MiddleName,
                    LastName = employeeInfoDB.LastName,
                    WorkHoursPerWeek = employeeInfoDB.WorkHoursPerWeek,
                    LoginID = employeeInfoDB.LoginID,
                    IAD_StartDate = employeeInfoDB.IAD_StartDate,
                    TimeSheetExempt = employeeInfoDB.TimeSheetExempt,
                    Allocation = employeeInfoDB.Allocation,
                    AIG_EmployeeNumber = employeeInfoDB.AIG_EmployeeNumber,
                    PhoneNumber = employeeInfoDB.PhoneNumber,
                    Email = employeeInfoDB.Email,
                    AIG_StartDate = employeeInfoDB.AIG_StartDate,
                    AIG_TerminationDate = employeeInfoDB.AIG_TerminationDate,
                    LOA_Date = employeeInfoDB.LOA_Date,
                    ReInstateDate = employeeInfoDB.ReInstateDate,
                    TotalHours = employeeInfoDB.TotalHours,
                    DepartmentInfo = new()
                    {
                        Id = employeeInfoDB.DepartmentId,
                        Name = employeeInfoDB.DepartmentName,
                    },
                    BudgetDepartmentInfo = new()
                    {
                        Id = employeeInfoDB.BudgetDepartmentId,
                        Name = employeeInfoDB.BudgetDepartmentName,
                    },
                    LocationInfo = new()
                    {
                        Id = employeeInfoDB.LocationId,
                        Name = employeeInfoDB.LocationName
                    },
                    GradeInfo = new()
                    {
                        Id = employeeInfoDB.GradeId,
                        Name = employeeInfoDB.GradeName
                    },
                    TitleInfo = new()
                    {
                        Id = employeeInfoDB.TitleId,
                        Name = employeeInfoDB.TitleName
                    },
                    EmployeeRoleInfo = new()
                    {
                        Id = employeeInfoDB.EmployeeRoleId,
                        Name = employeeInfoDB.EmployeeRoleName
                    },
                    DirectReportInfo = new()
                    {
                        Id = employeeInfoDB.DirectReportId,
                        FirstName = employeeInfoDB.FirstName,
                        LastName = employeeInfoDB.LastName,
                        MiddleName = employeeInfoDB.MiddleName,
                        TotalHours = employeeInfoDB.TotalHours,

                        // TODO Change employee details to Direct Report details
                        //GradeInfo = new()
                        //{
                        //    Id= employeeInfoDB.GradeId,
                        //    Name = employeeInfoDB.GradeName
                        //},
                        //LocationInfo = new()
                        //{
                        //    Id = employeeInfoDB.LocationId,
                        //    Name = employeeInfoDB.LocationName
                        //}
                    },
                    HireTypeInfo = new()
                    {
                        Id = employeeInfoDB.HireTypeId,
                        Name = employeeInfoDB.HireTypeName
                    },
                    ATS_AdminLevelTypeInfo = new()
                    {
                        Id = employeeInfoDB.ATS_Admin_LevelID,
                        Name = employeeInfoDB.ATS_Admin_Level
                    },
                    ATS_SecurityLevelTypeInfo = new()
                    {
                        Id = employeeInfoDB.ATS_Security_LevelID,
                        Name = employeeInfoDB.ATS_Security_Level
                    },
                    BPA_ManagementLevelTypeInfo = new()
                    {
                        Id = employeeInfoDB.BPA_Management_LevelID,
                        Name = employeeInfoDB.BPA_Management_Level
                    },
                    RITA_LevelInfo = new()
                    {
                        Id = employeeInfoDB.RITA_LevelID,
                        Name = employeeInfoDB.RITA_Level
                    },
                    BPA_LevelInfo = new()
                    {
                        Id = employeeInfoDB.BPA_LevelID,
                        Name = employeeInfoDB.BPA_Level
                    },
                    TerminationTypeInfo = new()
                    {
                        Id = employeeInfoDB.TerminationTypeId,
                        Name = employeeInfoDB.TerminationType
                    }
                });

            return response;

        }

    }
}
