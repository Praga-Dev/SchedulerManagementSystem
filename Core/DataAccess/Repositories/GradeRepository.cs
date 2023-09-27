using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private static List<GradeInfoDB> _GradeInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "Grade 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "Grade 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "Grade 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "Grade 4"},
            };


        //public readonly List<GradeInfoDB> GradeInfoList = _GradeInfoList;
        public async Task<Response<Guid>> CreateGrade(GradeInfoDB gradeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (gradeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    gradeInfoDB.Id = id;
                    _GradeInfoList.Add(gradeInfoDB);

                    response.Data = id;
                    response.IsSuccess = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<Guid>> UpdateGrade(GradeInfoDB gradeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (gradeInfoDB != null && Helpers.IsValidGuid(gradeInfoDB.Id))
                {
                    GradeInfoDB? grade = _GradeInfoList.FirstOrDefault(gr => gr.Id == gradeInfoDB.Id);
                    if (grade != null)
                    {
                        grade.Name = gradeInfoDB.Name;

                        response.Data = gradeInfoDB.Id;
                        response.IsSuccess = true;
                        response.Message = ResponseConstants.SUCCESS;
                        return response;
                    }

                    response.Message = ResponseConstants.FAILED;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GradeInfoDB>>> GetGrades(Guid loggedInUserId)
        {
            return new()
            {
                Data = _GradeInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<GradeInfoDB>> GetGradeById(Guid gradeId, Guid loggedInUserId)
        {
            Response<GradeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(gradeId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _GradeInfoList.FirstOrDefault(gr => gr.Id == gradeId) ?? new()
                    };
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<Guid>> DeleteGradeById(Guid gradeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(gradeId))
                {
                    _GradeInfoList = _GradeInfoList.Where(gr => gr.Id != gradeId).ToList();
                    response.Data = gradeId;
                    response.IsSuccess = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
