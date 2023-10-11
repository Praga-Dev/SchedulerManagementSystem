using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class HireTypeCommonMapper
    {
        public static HireTypeInfo GetHireTypeInfo(HireTypeInfoDB hireTypeInfoDB)
        {
            if (hireTypeInfoDB == null)
            {
                return new HireTypeInfo();
            }

            return new HireTypeInfo()
            {
                Id = hireTypeInfoDB.Id,
                Name = hireTypeInfoDB.Name
            };
        }

        public static HireTypeInfoDB GetHireTypeInfoDB(HireTypeInfo hireTypeInfo)
        {
            if (hireTypeInfo == null)
            {
                return new HireTypeInfoDB();
            }

            return new HireTypeInfoDB()
            {
                Id = hireTypeInfo.Id,
                Name = hireTypeInfo.Name
            };
        }

        public static List<HireTypeInfoDB> GetHireTypeInfoDBList(List<HireTypeInfo> hireTypeInfoList)
        {
            if (hireTypeInfoList == null)
            {
                return new List<HireTypeInfoDB>();
            }

            List<HireTypeInfoDB> response = new();

            response = hireTypeInfoList.ConvertAll(
                hireType => new HireTypeInfoDB
                {
                    Id = hireType.Id,
                    Name = hireType.Name
                });

            return response;

        }

        public static List<HireTypeInfo> GetHireTypeInfoList(List<HireTypeInfoDB> hireTypeInfoDBList)
        {
            if (hireTypeInfoDBList == null)
            {
                return new List<HireTypeInfo>();
            }

            List<HireTypeInfo> response = hireTypeInfoDBList.ConvertAll(
                hireType => new HireTypeInfo
                {
                    Id = hireType.Id,
                    Name = hireType.Name
                });

            return response;

        }

    }
}
