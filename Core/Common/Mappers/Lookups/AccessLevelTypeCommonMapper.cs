using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class AccessLevelTypeCommonMapper
    {
        public static AccessLevelTypeInfo GetAccessLevelTypeInfo(AccessLevelTypeInfoDB accessLevelTypeInfoDB)
        {
            if (accessLevelTypeInfoDB == null)
            {
                return new AccessLevelTypeInfo();
            }

            return new AccessLevelTypeInfo()
            {
                Id = accessLevelTypeInfoDB.Id,
                Name = accessLevelTypeInfoDB.Name
            };
        }

        public static AccessLevelTypeInfoDB GetAccessLevelTypeInfoDB(AccessLevelTypeInfo accessLevelTypeInfo)
        {
            if (accessLevelTypeInfo == null)
            {
                return new AccessLevelTypeInfoDB();
            }

            return new AccessLevelTypeInfoDB()
            {
                Id = accessLevelTypeInfo.Id,
                Name = accessLevelTypeInfo.Name
            };
        }

        public static List<AccessLevelTypeInfoDB> GetAccessLevelTypeInfoDBList(List<AccessLevelTypeInfo> accessLevelTypeInfoList)
        {
            if (accessLevelTypeInfoList == null)
            {
                return new List<AccessLevelTypeInfoDB>();
            }

            List<AccessLevelTypeInfoDB> response = new();

            response = accessLevelTypeInfoList.ConvertAll(
                accessLevelType => new AccessLevelTypeInfoDB
                {
                    Id = accessLevelType.Id,
                    Name = accessLevelType.Name
                });

            return response;

        }

        public static List<AccessLevelTypeInfo> GetAccessLevelTypeInfoList(List<AccessLevelTypeInfoDB> accessLevelTypeInfoDBList)
        {
            if (accessLevelTypeInfoDBList == null)
            {
                return new List<AccessLevelTypeInfo>();
            }

            List<AccessLevelTypeInfo> response = accessLevelTypeInfoDBList.ConvertAll(
                accessLevelType => new AccessLevelTypeInfo
                {
                    Id = accessLevelType.Id,
                    Name = accessLevelType.Name
                });

            return response;

        }

    }
}
