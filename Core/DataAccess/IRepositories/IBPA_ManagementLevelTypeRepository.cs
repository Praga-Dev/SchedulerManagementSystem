using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IBPA_ManagementLevelTypeRepository
    {
        Task<Response<Guid>> CreateBPA_ManagementLevelType(BPA_ManagementLevelTypeInfoDB bPA_ManagementLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateBPA_ManagementLevelType(BPA_ManagementLevelTypeInfoDB bPA_ManagementLevelTypeInfoDb, Guid loggedInUserId);
        Task<Response<List<BPA_ManagementLevelTypeInfoDB>>> GetBPA_ManagementLevelTypes(Guid loggedInUserId);
        Task<Response<BPA_ManagementLevelTypeInfoDB>> GetBPA_ManagementLevelTypeById(Guid bPA_ManagementLevelTypeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteBPA_ManagementLevelTypeById(Guid bPA_ManagementLevelTypeId, Guid loggedInUserId);
    }
}
