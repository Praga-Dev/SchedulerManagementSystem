using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface ITerminationTypeRepository
    {
        Task<Response<Guid>> CreateTerminationType(TerminationTypeInfoDB terminationTypeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateTerminationType(TerminationTypeInfoDB terminationTypeInfoDb, Guid loggedInUserId);
        Task<Response<List<TerminationTypeInfoDB>>> GetTerminationTypes(Guid loggedInUserId);
        Task<Response<TerminationTypeInfoDB>> GetTerminationTypeById(Guid terminationTypeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteTerminationTypeById(Guid terminationTypeId, Guid loggedInUserId);
    }
}
