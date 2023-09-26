using SchedulerManagementSystem.DataModels;
using SchedulerManagementSystem.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class LocationCommonMapper
    {
        public static LocationInfo GetLocationInfo(LocationInfoDB locationInfoDB)
        {
            if (locationInfoDB == null)
            {
                return new LocationInfo();
            }

            return new LocationInfo()
            {
                Id = locationInfoDB.Id,
                Name = locationInfoDB.Name
            };
        }

        public static LocationInfoDB GetLocationInfoDB(LocationInfo locationInfo)
        {
            if (locationInfo == null)
            {
                return new LocationInfoDB();
            }

            return new LocationInfoDB()
            {
                Id = locationInfo.Id,
                Name = locationInfo.Name
            };
        }

        public static List<LocationInfoDB> GetLocationInfoDBList(List<LocationInfo> locationInfoList)
        {
            if (locationInfoList == null)
            {
                return new List<LocationInfoDB>();
            }

            List<LocationInfoDB> response = new();

            response = locationInfoList.ConvertAll(
                location => new LocationInfoDB
                {
                    Id = location.Id,
                    Name = location.Name
                });

            return response;

        }

        public static List<LocationInfo> GetLocationInfoList(List<LocationInfoDB> locationInfoDBList)
        {
            if (locationInfoDBList == null)
            {
                return new List<LocationInfo>();
            }

            List<LocationInfo> response = locationInfoDBList.ConvertAll(
                location => new LocationInfo
                {
                    Id = location.Id,
                    Name = location.Name
                });

            return response;

        }

    }
}
