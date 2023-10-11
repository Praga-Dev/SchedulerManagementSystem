using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IATS_AdminLevelTypeRepository
    {
        Task<Response<Guid>> CreateATS_AdminLevelType(ATS_AdminLevelTypeInfoDB aTS_AdminLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateATS_AdminLevelType(ATS_AdminLevelTypeInfoDB aTS_AdminLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<List<ATS_AdminLevelTypeInfoDB>>> GetATS_AdminLevelTypes(Guid loggedInUserId);
        Task<Response<ATS_AdminLevelTypeInfoDB>> GetATS_AdminLevelTypeById(Guid aTS_AdminLevelTypeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteATS_AdminLevelTypeById(Guid aTS_AdminLevelTypeId, Guid loggedInUserId);
    }
}

