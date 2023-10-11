using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class TitleCommonMapper
    {
        public static TitleInfo GetTitleInfo(TitleInfoDB titleInfoDB)
        {
            if (titleInfoDB == null)
            {
                return new TitleInfo();
            }

            return new TitleInfo()
            {
                Id = titleInfoDB.Id,
                Name = titleInfoDB.Name
            };
        }

        public static TitleInfoDB GetTitleInfoDB(TitleInfo titleInfo)
        {
            if (titleInfo == null)
            {
                return new TitleInfoDB();
            }

            return new TitleInfoDB()
            {
                Id = titleInfo.Id,
                Name = titleInfo.Name
            };
        }

        public static List<TitleInfoDB> GetTitleInfoDBList(List<TitleInfo> titleInfoList)
        {
            if (titleInfoList == null)
            {
                return new List<TitleInfoDB>();
            }

            List<TitleInfoDB> response = new();

            response = titleInfoList.ConvertAll(
                title => new TitleInfoDB
                {
                    Id = title.Id,
                    Name = title.Name
                });

            return response;

        }

        public static List<TitleInfo> GetTitleInfoList(List<TitleInfoDB> titleInfoDBList)
        {
            if (titleInfoDBList == null)
            {
                return new List<TitleInfo>();
            }

            List<TitleInfo> response = titleInfoDBList.ConvertAll(
                title => new TitleInfo
                {
                    Id = title.Id,
                    Name = title.Name
                });

            return response;

        }

    }
}
