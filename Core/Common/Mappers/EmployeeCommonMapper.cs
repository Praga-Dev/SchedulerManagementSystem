using SchedulerManagementSystem.DataModels;
using SchedulerManagementSystem.Models;
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
                Name = employeeInfoDB.Name,
                GradeInfo = new()
                {
                    Id = employeeInfoDB.GradeId,
                    Name = employeeInfoDB.GradeName
                },
                LocationInfo = new()
                {
                    Id = employeeInfoDB.LocationId,
                    Name = employeeInfoDB.LocationName
                },
                TotalHours = employeeInfoDB.TotalHours
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
                Name = employeeInfo.Name,
                TotalHours = employeeInfo.TotalHours,
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
                employee => new EmployeeInfoDB
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    TotalHours = employee.TotalHours,   
                    GradeId = (employee.GradeInfo != null ? employee.GradeInfo.Id : Guid.Empty),
                    GradeName = (employee.GradeInfo != null ? employee.GradeInfo.Name : string.Empty) ?? string.Empty,
                    LocationId = (employee.LocationInfo != null ? employee.LocationInfo.Id : Guid.Empty),
                    LocationName = (employee.LocationInfo != null ? employee.LocationInfo.Name : string.Empty) ?? string.Empty,

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
                employee => new EmployeeInfo
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    GradeInfo = new()
                    {
                        Id = employee.GradeId,
                        Name = employee.GradeName
                    },
                    LocationInfo = new()
                    {
                        Id = employee.LocationId,
                        Name = employee.LocationName
                    },
                    TotalHours = employee.TotalHours
                });

            return response;

        }

    }
}
