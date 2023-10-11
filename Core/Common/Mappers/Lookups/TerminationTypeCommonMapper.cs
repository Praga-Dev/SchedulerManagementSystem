using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class TerminationTypeCommonMapper
    {
        public static TerminationTypeInfo GetTerminationTypeInfo(TerminationTypeInfoDB terminationTypeInfoDB)
        {
            if (terminationTypeInfoDB == null)
            {
                return new TerminationTypeInfo();
            }

            return new TerminationTypeInfo()
            {
                Id = terminationTypeInfoDB.Id,
                Name = terminationTypeInfoDB.Name
            };
        }

        public static TerminationTypeInfoDB GetTerminationTypeInfoDB(TerminationTypeInfo terminationTypeInfo)
        {
            if (terminationTypeInfo == null)
            {
                return new TerminationTypeInfoDB();
            }

            return new TerminationTypeInfoDB()
            {
                Id = terminationTypeInfo.Id,
                Name = terminationTypeInfo.Name
            };
        }

        public static List<TerminationTypeInfoDB> GetTerminationTypeInfoDBList(List<TerminationTypeInfo> terminationTypeInfoList)
        {
            if (terminationTypeInfoList == null)
            {
                return new List<TerminationTypeInfoDB>();
            }

            List<TerminationTypeInfoDB> response = new();

            response = terminationTypeInfoList.ConvertAll(
                terminationType => new TerminationTypeInfoDB
                {
                    Id = terminationType.Id,
                    Name = terminationType.Name
                });

            return response;

        }

        public static List<TerminationTypeInfo> GetTerminationTypeInfoList(List<TerminationTypeInfoDB> terminationTypeInfoDBList)
        {
            if (terminationTypeInfoDBList == null)
            {
                return new List<TerminationTypeInfo>();
            }

            List<TerminationTypeInfo> response = terminationTypeInfoDBList.ConvertAll(
                terminationType => new TerminationTypeInfo
                {
                    Id = terminationType.Id,
                    Name = terminationType.Name
                });

            return response;

        }

    }
}
