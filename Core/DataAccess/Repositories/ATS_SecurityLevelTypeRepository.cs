using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class ATS_SecurityLevelTypeRepository : IATS_SecurityLevelTypeRepository
    {
        private static List<ATS_SecurityLevelTypeInfoDB> _ATS_SecurityLevelTypeInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "ATS_SecurityLevelType 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "ATS_SecurityLevelType 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "ATS_SecurityLevelType 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "ATS_SecurityLevelType 4"},
            };


        //public readonly List<ATS_SecurityLevelTypeInfoDB> ATS_SecurityLevelTypeInfoList = _ATS_SecurityLevelTypeInfoList;
        public async Task<Response<Guid>> CreateATS_SecurityLevelType(ATS_SecurityLevelTypeInfoDB aTS_SecurityLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (aTS_SecurityLevelTypeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    aTS_SecurityLevelTypeInfoDB.Id = id;
                    _ATS_SecurityLevelTypeInfoList.Add(aTS_SecurityLevelTypeInfoDB);

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

        public async Task<Response<Guid>> UpdateATS_SecurityLevelType(ATS_SecurityLevelTypeInfoDB aTS_SecurityLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (aTS_SecurityLevelTypeInfoDB != null && Helpers.IsValidGuid(aTS_SecurityLevelTypeInfoDB.Id))
                {
                    ATS_SecurityLevelTypeInfoDB? aTS_SecurityLevelType = _ATS_SecurityLevelTypeInfoList.FirstOrDefault(gr => gr.Id == aTS_SecurityLevelTypeInfoDB.Id);
                    if (aTS_SecurityLevelType != null)
                    {
                        aTS_SecurityLevelType.Name = aTS_SecurityLevelTypeInfoDB.Name;

                        response.Data = aTS_SecurityLevelTypeInfoDB.Id;
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

        public async Task<Response<List<ATS_SecurityLevelTypeInfoDB>>> GetATS_SecurityLevelTypes(Guid loggedInUserId)
        {
            return new()
            {
                Data = _ATS_SecurityLevelTypeInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<ATS_SecurityLevelTypeInfoDB>> GetATS_SecurityLevelTypeById(Guid aTS_SecurityLevelTypeId, Guid loggedInUserId)
        {
            Response<ATS_SecurityLevelTypeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(aTS_SecurityLevelTypeId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _ATS_SecurityLevelTypeInfoList.FirstOrDefault(gr => gr.Id == aTS_SecurityLevelTypeId) ?? new()
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

        public async Task<Response<Guid>> DeleteATS_SecurityLevelTypeById(Guid aTS_SecurityLevelTypeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(aTS_SecurityLevelTypeId))
                {
                    _ATS_SecurityLevelTypeInfoList = _ATS_SecurityLevelTypeInfoList.Where(gr => gr.Id != aTS_SecurityLevelTypeId).ToList();
                    response.Data = aTS_SecurityLevelTypeId;
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
