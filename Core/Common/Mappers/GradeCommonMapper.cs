using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulerManagementSystem.Common.Mappers
{
    public static class GradeCommonMapper
    {
        public static GradeInfo GetGradeInfo(GradeInfoDB gradeInfoDB)
        {
            if (gradeInfoDB == null)
            {
                return new GradeInfo();
            }

            return new GradeInfo()
            {
                Id = gradeInfoDB.Id,
                Name = gradeInfoDB.Name
            };
        }

        public static GradeInfoDB GetGradeInfoDB(GradeInfo gradeInfo)
        {
            if (gradeInfo == null)
            {
                return new GradeInfoDB();
            }

            return new GradeInfoDB()
            {
                Id = gradeInfo.Id,
                Name = gradeInfo.Name
            };
        }

        public static List<GradeInfoDB> GetGradeInfoDBList(List<GradeInfo> gradeInfoList)
        {
            if (gradeInfoList == null)
            {
                return new List<GradeInfoDB>();
            }

            List<GradeInfoDB> response = new();

            response = gradeInfoList.ConvertAll(
                grade => new GradeInfoDB
                {
                    Id = grade.Id,
                    Name = grade.Name
                });

            return response;
            
        }

        public static List<GradeInfo> GetGradeInfoList(List<GradeInfoDB> gradeInfoDBList)
        {
            if (gradeInfoDBList == null)
            {
                return new List<GradeInfo>();
            }

            List<GradeInfo> response = gradeInfoDBList.ConvertAll(
                grade => new GradeInfo
                {
                    Id = grade.Id,
                    Name = grade.Name
                });

            return response;

        }

    }
}
