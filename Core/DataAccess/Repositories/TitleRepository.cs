using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private static List<TitleInfoDB> _TitleInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "Title 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "Title 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "Title 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "Title 4"},
            };


        //public readonly List<TitleInfoDB> TitleInfoList = _TitleInfoList;
        public async Task<Response<Guid>> CreateTitle(TitleInfoDB titleInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (titleInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    titleInfoDB.Id = id;
                    _TitleInfoList.Add(titleInfoDB);

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

        public async Task<Response<Guid>> UpdateTitle(TitleInfoDB titleInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (titleInfoDB != null && Helpers.IsValidGuid(titleInfoDB.Id))
                {
                    TitleInfoDB? title = _TitleInfoList.FirstOrDefault(gr => gr.Id == titleInfoDB.Id);
                    if (title != null)
                    {
                        title.Name = titleInfoDB.Name;

                        response.Data = titleInfoDB.Id;
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

        public async Task<Response<List<TitleInfoDB>>> GetTitles(Guid loggedInUserId)
        {
            return new()
            {
                Data = _TitleInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<TitleInfoDB>> GetTitleById(Guid titleId, Guid loggedInUserId)
        {
            Response<TitleInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(titleId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _TitleInfoList.FirstOrDefault(gr => gr.Id == titleId) ?? new()
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

        public async Task<Response<Guid>> DeleteTitleById(Guid titleId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(titleId))
                {
                    _TitleInfoList = _TitleInfoList.Where(gr => gr.Id != titleId).ToList();
                    response.Data = titleId;
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
