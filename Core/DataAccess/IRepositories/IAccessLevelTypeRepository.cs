using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IAccessLevelTypeRepository
    {
        Task<Response<Guid>> CreateAccessLevelType(AccessLevelTypeInfoDB accessLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateAccessLevelType(AccessLevelTypeInfoDB accessLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<List<AccessLevelTypeInfoDB>>> GetAccessLevelTypes(Guid loggedInUserId);
        Task<Response<AccessLevelTypeInfoDB>> GetAccessLevelTypeById(Guid accessLevelTypeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteAccessLevelTypeById(Guid accessLevelTypeId, Guid loggedInUserId);
    }
}
