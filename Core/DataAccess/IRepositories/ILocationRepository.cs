using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface ILocationRepository
    {
        Task<Response<Guid>> CreateLocation(LocationInfoDB LocationInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateLocation(LocationInfoDB LocationInfoDb, Guid loggedInUserId);
        Task<Response<List<LocationInfoDB>>> GetLocations(Guid loggedInUserId);
        Task<Response<LocationInfoDB>> GetLocationById(Guid locationId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteLocationById(Guid locationId, Guid loggedInUserId);
    }
}
