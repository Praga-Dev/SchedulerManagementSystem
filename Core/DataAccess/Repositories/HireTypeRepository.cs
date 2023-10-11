using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class HireTypeRepository : IHireTypeRepository
    {
        private static List<HireTypeInfoDB> _HireTypeInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "HireType 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "HireType 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "HireType 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "HireType 4"},
            };


        //public readonly List<HireTypeInfoDB> HireTypeInfoList = _HireTypeInfoList;
        public async Task<Response<Guid>> CreateHireType(HireTypeInfoDB hireTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (hireTypeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    hireTypeInfoDB.Id = id;
                    _HireTypeInfoList.Add(hireTypeInfoDB);

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

        public async Task<Response<Guid>> UpdateHireType(HireTypeInfoDB hireTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (hireTypeInfoDB != null && Helpers.IsValidGuid(hireTypeInfoDB.Id))
                {
                    HireTypeInfoDB? hireType = _HireTypeInfoList.FirstOrDefault(gr => gr.Id == hireTypeInfoDB.Id);
                    if (hireType != null)
                    {
                        hireType.Name = hireTypeInfoDB.Name;

                        response.Data = hireTypeInfoDB.Id;
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

        public async Task<Response<List<HireTypeInfoDB>>> GetHireTypes(Guid loggedInUserId)
        {
            return new()
            {
                Data = _HireTypeInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<HireTypeInfoDB>> GetHireTypeById(Guid hireTypeId, Guid loggedInUserId)
        {
            Response<HireTypeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(hireTypeId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _HireTypeInfoList.FirstOrDefault(gr => gr.Id == hireTypeId) ?? new()
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

        public async Task<Response<Guid>> DeleteHireTypeById(Guid hireTypeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(hireTypeId))
                {
                    _HireTypeInfoList = _HireTypeInfoList.Where(gr => gr.Id != hireTypeId).ToList();
                    response.Data = hireTypeId;
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
