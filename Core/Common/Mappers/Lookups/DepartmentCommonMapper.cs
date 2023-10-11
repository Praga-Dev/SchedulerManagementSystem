using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class DepartmentCommonMapper
    {
        public static DepartmentInfo GetDepartmentInfo(DepartmentInfoDB departmentInfoDB)
        {
            if (departmentInfoDB == null)
            {
                return new DepartmentInfo();
            }

            return new DepartmentInfo()
            {
                Id = departmentInfoDB.Id,
                Name = departmentInfoDB.Name
            };
        }

        public static DepartmentInfoDB GetDepartmentInfoDB(DepartmentInfo departmentInfo)
        {
            if (departmentInfo == null)
            {
                return new DepartmentInfoDB();
            }

            return new DepartmentInfoDB()
            {
                Id = departmentInfo.Id,
                Name = departmentInfo.Name
            };
        }

        public static List<DepartmentInfoDB> GetDepartmentInfoDBList(List<DepartmentInfo> departmentInfoList)
        {
            if (departmentInfoList == null)
            {
                return new List<DepartmentInfoDB>();
            }

            List<DepartmentInfoDB> response = new();

            response = departmentInfoList.ConvertAll(
                department => new DepartmentInfoDB
                {
                    Id = department.Id,
                    Name = department.Name
                });

            return response;

        }

        public static List<DepartmentInfo> GetDepartmentInfoList(List<DepartmentInfoDB> departmentInfoDBList)
        {
            if (departmentInfoDBList == null)
            {
                return new List<DepartmentInfo>();
            }

            List<DepartmentInfo> response = departmentInfoDBList.ConvertAll(
                department => new DepartmentInfo
                {
                    Id = department.Id,
                    Name = department.Name
                });

            return response;

        }

    }
}
