using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class BPA_ManagementLevelTypeRepository : IBPA_ManagementLevelTypeRepository
    {
        private static List<BPA_ManagementLevelTypeInfoDB> _BPA_ManagementLevelTypeInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "BPA_ManagementLevelType 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "BPA_ManagementLevelType 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "BPA_ManagementLevelType 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "BPA_ManagementLevelType 4"},
            };


        //public readonly List<BPA_ManagementLevelTypeInfoDB> BPA_ManagementLevelTypeInfoList = _BPA_ManagementLevelTypeInfoList;
        public async Task<Response<Guid>> CreateBPA_ManagementLevelType(BPA_ManagementLevelTypeInfoDB bPA_ManagementLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (bPA_ManagementLevelTypeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    bPA_ManagementLevelTypeInfoDB.Id = id;
                    _BPA_ManagementLevelTypeInfoList.Add(bPA_ManagementLevelTypeInfoDB);

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

        public async Task<Response<Guid>> UpdateBPA_ManagementLevelType(BPA_ManagementLevelTypeInfoDB bPA_ManagementLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (bPA_ManagementLevelTypeInfoDB != null && Helpers.IsValidGuid(bPA_ManagementLevelTypeInfoDB.Id))
                {
                    BPA_ManagementLevelTypeInfoDB? bPA_ManagementLevelType = _BPA_ManagementLevelTypeInfoList.FirstOrDefault(gr => gr.Id == bPA_ManagementLevelTypeInfoDB.Id);
                    if (bPA_ManagementLevelType != null)
                    {
                        bPA_ManagementLevelType.Name = bPA_ManagementLevelTypeInfoDB.Name;

                        response.Data = bPA_ManagementLevelTypeInfoDB.Id;
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

        public async Task<Response<List<BPA_ManagementLevelTypeInfoDB>>> GetBPA_ManagementLevelTypes(Guid loggedInUserId)
        {
            return new()
            {
                Data = _BPA_ManagementLevelTypeInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<BPA_ManagementLevelTypeInfoDB>> GetBPA_ManagementLevelTypeById(Guid bPA_ManagementLevelTypeId, Guid loggedInUserId)
        {
            Response<BPA_ManagementLevelTypeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(bPA_ManagementLevelTypeId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _BPA_ManagementLevelTypeInfoList.FirstOrDefault(gr => gr.Id == bPA_ManagementLevelTypeId) ?? new()
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

        public async Task<Response<Guid>> DeleteBPA_ManagementLevelTypeById(Guid bPA_ManagementLevelTypeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(bPA_ManagementLevelTypeId))
                {
                    _BPA_ManagementLevelTypeInfoList = _BPA_ManagementLevelTypeInfoList.Where(gr => gr.Id != bPA_ManagementLevelTypeId).ToList();
                    response.Data = bPA_ManagementLevelTypeId;
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
