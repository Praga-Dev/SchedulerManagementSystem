using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class ATS_AdminLevelTypeCommonMapper
    {
        public static ATS_AdminLevelTypeInfo GetATS_AdminLevelTypeInfo(ATS_AdminLevelTypeInfoDB aTS_AdminLevelTypeInfoDB)
        {
            if (aTS_AdminLevelTypeInfoDB == null)
            {
                return new ATS_AdminLevelTypeInfo();
            }

            return new ATS_AdminLevelTypeInfo()
            {
                Id = aTS_AdminLevelTypeInfoDB.Id,
                Name = aTS_AdminLevelTypeInfoDB.Name
            };
        }

        public static ATS_AdminLevelTypeInfoDB GetATS_AdminLevelTypeInfoDB(ATS_AdminLevelTypeInfo aTS_AdminLevelTypeInfo)
        {
            if (aTS_AdminLevelTypeInfo == null)
            {
                return new ATS_AdminLevelTypeInfoDB();
            }

            return new ATS_AdminLevelTypeInfoDB()
            {
                Id = aTS_AdminLevelTypeInfo.Id,
                Name = aTS_AdminLevelTypeInfo.Name
            };
        }

        public static List<ATS_AdminLevelTypeInfoDB> GetATS_AdminLevelTypeInfoDBList(List<ATS_AdminLevelTypeInfo> aTS_AdminLevelTypeInfoList)
        {
            if (aTS_AdminLevelTypeInfoList == null)
            {
                return new List<ATS_AdminLevelTypeInfoDB>();
            }

            List<ATS_AdminLevelTypeInfoDB> response = new();

            response = aTS_AdminLevelTypeInfoList.ConvertAll(
                aTS_AdminLevelType => new ATS_AdminLevelTypeInfoDB
                {
                    Id = aTS_AdminLevelType.Id,
                    Name = aTS_AdminLevelType.Name
                });

            return response;

        }

        public static List<ATS_AdminLevelTypeInfo> GetATS_AdminLevelTypeInfoList(List<ATS_AdminLevelTypeInfoDB> aTS_AdminLevelTypeInfoDBList)
        {
            if (aTS_AdminLevelTypeInfoDBList == null)
            {
                return new List<ATS_AdminLevelTypeInfo>();
            }

            List<ATS_AdminLevelTypeInfo> response = aTS_AdminLevelTypeInfoDBList.ConvertAll(
                aTS_AdminLevelType => new ATS_AdminLevelTypeInfo
                {
                    Id = aTS_AdminLevelType.Id,
                    Name = aTS_AdminLevelType.Name
                });

            return response;

        }

    }
}
