using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface ITitleRepository
    {
        Task<Response<Guid>> CreateTitle(TitleInfoDB titleInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateTitle(TitleInfoDB titleInfoDb, Guid loggedInUserId);
        Task<Response<List<TitleInfoDB>>> GetTitles(Guid loggedInUserId);
        Task<Response<TitleInfoDB>> GetTitleById(Guid titleId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteTitleById(Guid titleId, Guid loggedInUserId);
    }
}
