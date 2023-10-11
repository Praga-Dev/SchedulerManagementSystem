using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IATS_SecurityLevelTypeRepository
    {
        Task<Response<Guid>> CreateATS_SecurityLevelType(ATS_SecurityLevelTypeInfoDB aTS_SecurityLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateATS_SecurityLevelType(ATS_SecurityLevelTypeInfoDB aTS_SecurityLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<List<ATS_SecurityLevelTypeInfoDB>>> GetATS_SecurityLevelTypes(Guid loggedInUserId);
        Task<Response<ATS_SecurityLevelTypeInfoDB>> GetATS_SecurityLevelTypeById(Guid aTS_SecurityLevelTypeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteATS_SecurityLevelTypeById(Guid aTS_SecurityLevelTypeId, Guid loggedInUserId);
    }
}

