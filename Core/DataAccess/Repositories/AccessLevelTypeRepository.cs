using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class AccessLevelTypeRepository : IAccessLevelTypeRepository
    {
        private static List<AccessLevelTypeInfoDB> _AccessLevelTypeInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "AccessLevelType 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "AccessLevelType 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "AccessLevelType 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "AccessLevelType 4"},
            };


        //public readonly List<AccessLevelTypeInfoDB> AccessLevelTypeInfoList = _AccessLevelTypeInfoList;
        public async Task<Response<Guid>> CreateAccessLevelType(AccessLevelTypeInfoDB accessLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (accessLevelTypeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    accessLevelTypeInfoDB.Id = id;
                    _AccessLevelTypeInfoList.Add(accessLevelTypeInfoDB);

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

        public async Task<Response<Guid>> UpdateAccessLevelType(AccessLevelTypeInfoDB accessLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (accessLevelTypeInfoDB != null && Helpers.IsValidGuid(accessLevelTypeInfoDB.Id))
                {
                    AccessLevelTypeInfoDB? accessLevelType = _AccessLevelTypeInfoList.FirstOrDefault(gr => gr.Id == accessLevelTypeInfoDB.Id);
                    if (accessLevelType != null)
                    {
                        accessLevelType.Name = accessLevelTypeInfoDB.Name;

                        response.Data = accessLevelTypeInfoDB.Id;
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

        public async Task<Response<List<AccessLevelTypeInfoDB>>> GetAccessLevelTypes(Guid loggedInUserId)
        {
            return new()
            {
                Data = _AccessLevelTypeInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<AccessLevelTypeInfoDB>> GetAccessLevelTypeById(Guid accessLevelTypeId, Guid loggedInUserId)
        {
            Response<AccessLevelTypeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(accessLevelTypeId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _AccessLevelTypeInfoList.FirstOrDefault(gr => gr.Id == accessLevelTypeId) ?? new()
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

        public async Task<Response<Guid>> DeleteAccessLevelTypeById(Guid accessLevelTypeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(accessLevelTypeId))
                {
                    _AccessLevelTypeInfoList = _AccessLevelTypeInfoList.Where(gr => gr.Id != accessLevelTypeId).ToList();
                    response.Data = accessLevelTypeId;
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
