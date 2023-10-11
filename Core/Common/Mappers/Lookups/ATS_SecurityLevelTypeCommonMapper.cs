using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class ATS_SecurityLevelTypeCommonMapper
    {
        public static ATS_SecurityLevelTypeInfo GetATS_SecurityLevelTypeInfo(ATS_SecurityLevelTypeInfoDB aTS_SecurityLevelTypeInfoDB)
        {
            if (aTS_SecurityLevelTypeInfoDB == null)
            {
                return new ATS_SecurityLevelTypeInfo();
            }

            return new ATS_SecurityLevelTypeInfo()
            {
                Id = aTS_SecurityLevelTypeInfoDB.Id,
                Name = aTS_SecurityLevelTypeInfoDB.Name
            };
        }

        public static ATS_SecurityLevelTypeInfoDB GetATS_SecurityLevelTypeInfoDB(ATS_SecurityLevelTypeInfo aTS_SecurityLevelTypeInfo)
        {
            if (aTS_SecurityLevelTypeInfo == null)
            {
                return new ATS_SecurityLevelTypeInfoDB();
            }

            return new ATS_SecurityLevelTypeInfoDB()
            {
                Id = aTS_SecurityLevelTypeInfo.Id,
                Name = aTS_SecurityLevelTypeInfo.Name
            };
        }

        public static List<ATS_SecurityLevelTypeInfoDB> GetATS_SecurityLevelTypeInfoDBList(List<ATS_SecurityLevelTypeInfo> aTS_SecurityLevelTypeInfoList)
        {
            if (aTS_SecurityLevelTypeInfoList == null)
            {
                return new List<ATS_SecurityLevelTypeInfoDB>();
            }

            List<ATS_SecurityLevelTypeInfoDB> response = new();

            response = aTS_SecurityLevelTypeInfoList.ConvertAll(
                aTS_SecurityLevelType => new ATS_SecurityLevelTypeInfoDB
                {
                    Id = aTS_SecurityLevelType.Id,
                    Name = aTS_SecurityLevelType.Name
                });

            return response;

        }

        public static List<ATS_SecurityLevelTypeInfo> GetATS_SecurityLevelTypeInfoList(List<ATS_SecurityLevelTypeInfoDB> aTS_SecurityLevelTypeInfoDBList)
        {
            if (aTS_SecurityLevelTypeInfoDBList == null)
            {
                return new List<ATS_SecurityLevelTypeInfo>();
            }

            List<ATS_SecurityLevelTypeInfo> response = aTS_SecurityLevelTypeInfoDBList.ConvertAll(
                aTS_SecurityLevelType => new ATS_SecurityLevelTypeInfo
                {
                    Id = aTS_SecurityLevelType.Id,
                    Name = aTS_SecurityLevelType.Name
                });

            return response;

        }

    }
}
