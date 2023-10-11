using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IHireTypeRepository
    {
        Task<Response<Guid>> CreateHireType(HireTypeInfoDB hireTypeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateHireType(HireTypeInfoDB hireTypeInfoDb, Guid loggedInUserId);
        Task<Response<List<HireTypeInfoDB>>> GetHireTypes(Guid loggedInUserId);
        Task<Response<HireTypeInfoDB>> GetHireTypeById(Guid hireTypeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteHireTypeById(Guid hireTypeId, Guid loggedInUserId);
    }
}
