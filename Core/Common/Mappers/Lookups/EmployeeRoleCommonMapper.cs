using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class EmployeeRoleCommonMapper
    {
        public static EmployeeRoleInfo GetEmployeeRoleInfo(EmployeeRoleInfoDB employeeRoleInfoDB)
        {
            if (employeeRoleInfoDB == null)
            {
                return new EmployeeRoleInfo();
            }

            return new EmployeeRoleInfo()
            {
                Id = employeeRoleInfoDB.Id,
                Name = employeeRoleInfoDB.Name
            };
        }

        public static EmployeeRoleInfoDB GetEmployeeRoleInfoDB(EmployeeRoleInfo employeeRoleInfo)
        {
            if (employeeRoleInfo == null)
            {
                return new EmployeeRoleInfoDB();
            }

            return new EmployeeRoleInfoDB()
            {
                Id = employeeRoleInfo.Id,
                Name = employeeRoleInfo.Name
            };
        }

        public static List<EmployeeRoleInfoDB> GetEmployeeRoleInfoDBList(List<EmployeeRoleInfo> employeeRoleInfoList)
        {
            if (employeeRoleInfoList == null)
            {
                return new List<EmployeeRoleInfoDB>();
            }

            List<EmployeeRoleInfoDB> response = new();

            response = employeeRoleInfoList.ConvertAll(
                employeeRole => new EmployeeRoleInfoDB
                {
                    Id = employeeRole.Id,
                    Name = employeeRole.Name
                });

            return response;

        }

        public static List<EmployeeRoleInfo> GetEmployeeRoleInfoList(List<EmployeeRoleInfoDB> employeeRoleInfoDBList)
        {
            if (employeeRoleInfoDBList == null)
            {
                return new List<EmployeeRoleInfo>();
            }

            List<EmployeeRoleInfo> response = employeeRoleInfoDBList.ConvertAll(
                employeeRole => new EmployeeRoleInfo
                {
                    Id = employeeRole.Id,
                    Name = employeeRole.Name
                });

            return response;

        }

    }
}
