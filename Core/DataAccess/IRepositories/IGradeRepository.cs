using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IGradeRepository
    {
        Task<Response<Guid>> CreateGrade(GradeInfoDB gradeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateGrade(GradeInfoDB gradeInfoDb, Guid loggedInUserId);
        Task<Response<List<GradeInfoDB>>> GetGrades(Guid loggedInUserId);
        Task<Response<GradeInfoDB>> GetGradeById(Guid gradeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteGradeById(Guid gradeId, Guid loggedInUserId);
    }
}
