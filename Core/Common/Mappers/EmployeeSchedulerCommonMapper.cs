using SchedulerManagementSystem.DataModels;
using SchedulerManagementSystem.Models;
using SchedulerManagementSystem.DataModels.Scheduler;
using SchedulerManagementSystem.Models.Scheduler;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class EmployeeSchedulerCommonMapper
    {
        public static EmployeeSchedulerInfo GetEmployeeSchedulerInfo(EmployeeSchedulerInfoDB employeeSchedulerInfoDB)
        {
            if (employeeSchedulerInfoDB == null)
            {
                return new EmployeeSchedulerInfo();
            }

            return new EmployeeSchedulerInfo()
            {
                Id = employeeSchedulerInfoDB.Id,
                AllocatedHours = employeeSchedulerInfoDB.AllocatedHours,
                AvailableHours = employeeSchedulerInfoDB.AvailableHours,
                EmployeeInfoId = employeeSchedulerInfoDB.EmployeeInfoId,
                WorkScheduledDate = employeeSchedulerInfoDB.WorkScheduledDate
            };
        }

        public static EmployeeSchedulerInfoDB GetEmployeeSchedulerInfoDB(EmployeeSchedulerInfo employeeSchedulerInfo)
        {
            if (employeeSchedulerInfo == null)
            {
                return new EmployeeSchedulerInfoDB();
            }

            var employeeSchedulerInfoDB = new EmployeeSchedulerInfoDB()
            {
                Id = employeeSchedulerInfo.Id,
                AllocatedHours = employeeSchedulerInfo.AllocatedHours,
                AvailableHours = employeeSchedulerInfo.AvailableHours,
                EmployeeInfoId = employeeSchedulerInfo.EmployeeInfoId,
                WorkScheduledDate = employeeSchedulerInfo.WorkScheduledDate
            };

            return employeeSchedulerInfoDB;
        }

        public static List<EmployeeSchedulerInfoDB> GetEmployeeSchedulerInfoDBList(List<EmployeeSchedulerInfo> employeeSchedulerInfoList)
        {
            if (employeeSchedulerInfoList == null)
            {
                return new List<EmployeeSchedulerInfoDB>();
            }

            List<EmployeeSchedulerInfoDB> response = new();

            response = employeeSchedulerInfoList.ConvertAll(
                employee => new EmployeeSchedulerInfoDB
                {
                    Id = employee.Id,
                    AllocatedHours = employee.AllocatedHours,
                    AvailableHours = employee.AvailableHours,
                    EmployeeInfoId = employee.EmployeeInfoId,
                    WorkScheduledDate = employee.WorkScheduledDate
                });

            return response;

        }

        public static List<EmployeeSchedulerInfo> GetEmployeeSchedulerInfoList(List<EmployeeSchedulerInfoDB> employeeSchedulerInfoDBList)
        {
            if (employeeSchedulerInfoDBList == null)
            {
                return new List<EmployeeSchedulerInfo>();
            }

            List<EmployeeSchedulerInfo> response = employeeSchedulerInfoDBList.ConvertAll(
                employee => new EmployeeSchedulerInfo
                {
                    Id = employee.Id,
                    AllocatedHours = employee.AllocatedHours,
                    AvailableHours = employee.AvailableHours,
                    EmployeeInfoId = employee.EmployeeInfoId,
                    WorkScheduledDate = employee.WorkScheduledDate
                });

            return response;

        }

        #region EmployeeSchedulerVM Mapper

        public static List<EmployeeSchedulerVM> GetEmployeeSchedulerVM(List<EmployeeSchedulerInfo> employeeSchedulerInfoList, List<EmployeeInfo> employeeInfoList)
        {
            var result = new List<EmployeeSchedulerVM>();

            try
            {
                if (employeeSchedulerInfoList != null && employeeSchedulerInfoList.Any()
                    && employeeInfoList != null && employeeInfoList.Any())
                {
                    foreach (var employee in employeeInfoList)
                    {
                        EmployeeSchedulerVM employeeSchedulerVM = new()
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            MiddleName = employee.MiddleName,
                            LastName = employee.LastName,
                            GradeInfo = employee.GradeInfo,
                            LocationInfo = employee.LocationInfo,
                            TotalHours = employee.TotalHours,                            
                            EmployeeSchedulerInfoList = employeeSchedulerInfoList.Where(sch => sch.EmployeeInfoId == employee.Id).ToList()
                        };

                        result.Add(employeeSchedulerVM);
                    }
                }
            }
            catch (Exception ex)
            {
                // handle
            }

            return result;
        }

        #endregion


        #region CalendarEmployee

        public static List<CalendarEmployeeInfo> GetCalendarEmployeeInfoList(List<CalendarEmployeeInfoDB> calendarEmployeeInfoDBList)
        {
            if (calendarEmployeeInfoDBList == null)
            {
                return new List<CalendarEmployeeInfo>();
            }

            List<CalendarEmployeeInfo> response = calendarEmployeeInfoDBList
                .ConvertAll(clEmp => new CalendarEmployeeInfo
                {
                    Id = clEmp.Id,
                    FirstName = clEmp.FirstName,
                    MiddleName = clEmp.MiddleName,
                    LastName = clEmp.LastName,
                    LocationInfo = new()
                    {
                        Id = clEmp.LocationId,
                        Name = clEmp.LocationName
                    },
                    GradeInfo = new()
                    {
                        Id = clEmp.GradeId,
                        Name = clEmp.GradeName
                    },                    
                    TotalHours = clEmp.TotalHours,
                    IsAddedToCalendar = clEmp.IsAddedToCalendar                    
                });

            return response;
        }


        #endregion

    }
}
