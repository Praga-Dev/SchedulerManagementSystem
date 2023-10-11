using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class BPA_ManagementLevelTypeCommonMapper
    {
        public static BPA_ManagementLevelTypeInfo GetBPA_ManagementLevelTypeInfo(BPA_ManagementLevelTypeInfoDB bPA_ManagementLevelTypeInfoDB)
        {
            if (bPA_ManagementLevelTypeInfoDB == null)
            {
                return new BPA_ManagementLevelTypeInfo();
            }

            return new BPA_ManagementLevelTypeInfo()
            {
                Id = bPA_ManagementLevelTypeInfoDB.Id,
                Name = bPA_ManagementLevelTypeInfoDB.Name
            };
        }

        public static BPA_ManagementLevelTypeInfoDB GetBPA_ManagementLevelTypeInfoDB(BPA_ManagementLevelTypeInfo bPA_ManagementLevelTypeInfo)
        {
            if (bPA_ManagementLevelTypeInfo == null)
            {
                return new BPA_ManagementLevelTypeInfoDB();
            }

            return new BPA_ManagementLevelTypeInfoDB()
            {
                Id = bPA_ManagementLevelTypeInfo.Id,
                Name = bPA_ManagementLevelTypeInfo.Name
            };
        }

        public static List<BPA_ManagementLevelTypeInfoDB> GetBPA_ManagementLevelTypeInfoDBList(List<BPA_ManagementLevelTypeInfo> bPA_ManagementLevelTypeInfoList)
        {
            if (bPA_ManagementLevelTypeInfoList == null)
            {
                return new List<BPA_ManagementLevelTypeInfoDB>();
            }

            List<BPA_ManagementLevelTypeInfoDB> response = new();

            response = bPA_ManagementLevelTypeInfoList.ConvertAll(
                bPA_ManagementLevelType => new BPA_ManagementLevelTypeInfoDB
                {
                    Id = bPA_ManagementLevelType.Id,
                    Name = bPA_ManagementLevelType.Name
                });

            return response;

        }

        public static List<BPA_ManagementLevelTypeInfo> GetBPA_ManagementLevelTypeInfoList(List<BPA_ManagementLevelTypeInfoDB> bPA_ManagementLevelTypeInfoDBList)
        {
            if (bPA_ManagementLevelTypeInfoDBList == null)
            {
                return new List<BPA_ManagementLevelTypeInfo>();
            }

            List<BPA_ManagementLevelTypeInfo> response = bPA_ManagementLevelTypeInfoDBList.ConvertAll(
                bPA_ManagementLevelType => new BPA_ManagementLevelTypeInfo
                {
                    Id = bPA_ManagementLevelType.Id,
                    Name = bPA_ManagementLevelType.Name
                });

            return response;

        }

    }
}
