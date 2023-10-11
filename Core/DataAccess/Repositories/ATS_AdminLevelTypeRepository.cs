using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class ATS_AdminLevelTypeRepository : IATS_AdminLevelTypeRepository
    {
        private static List<ATS_AdminLevelTypeInfoDB> _ATS_AdminLevelTypeInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "ATS_AdminLevelType 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "ATS_AdminLevelType 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "ATS_AdminLevelType 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "ATS_AdminLevelType 4"},
            };


        //public readonly List<ATS_AdminLevelTypeInfoDB> ATS_AdminLevelTypeInfoList = _ATS_AdminLevelTypeInfoList;
        public async Task<Response<Guid>> CreateATS_AdminLevelType(ATS_AdminLevelTypeInfoDB aTS_AdminLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (aTS_AdminLevelTypeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    aTS_AdminLevelTypeInfoDB.Id = id;
                    _ATS_AdminLevelTypeInfoList.Add(aTS_AdminLevelTypeInfoDB);

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

        public async Task<Response<Guid>> UpdateATS_AdminLevelType(ATS_AdminLevelTypeInfoDB aTS_AdminLevelTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (aTS_AdminLevelTypeInfoDB != null && Helpers.IsValidGuid(aTS_AdminLevelTypeInfoDB.Id))
                {
                    ATS_AdminLevelTypeInfoDB? aTS_AdminLevelType = _ATS_AdminLevelTypeInfoList.FirstOrDefault(gr => gr.Id == aTS_AdminLevelTypeInfoDB.Id);
                    if (aTS_AdminLevelType != null)
                    {
                        aTS_AdminLevelType.Name = aTS_AdminLevelTypeInfoDB.Name;

                        response.Data = aTS_AdminLevelTypeInfoDB.Id;
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

        public async Task<Response<List<ATS_AdminLevelTypeInfoDB>>> GetATS_AdminLevelTypes(Guid loggedInUserId)
        {
            return new()
            {
                Data = _ATS_AdminLevelTypeInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<ATS_AdminLevelTypeInfoDB>> GetATS_AdminLevelTypeById(Guid aTS_AdminLevelTypeId, Guid loggedInUserId)
        {
            Response<ATS_AdminLevelTypeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(aTS_AdminLevelTypeId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _ATS_AdminLevelTypeInfoList.FirstOrDefault(gr => gr.Id == aTS_AdminLevelTypeId) ?? new()
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

        public async Task<Response<Guid>> DeleteATS_AdminLevelTypeById(Guid aTS_AdminLevelTypeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(aTS_AdminLevelTypeId))
                {
                    _ATS_AdminLevelTypeInfoList = _ATS_AdminLevelTypeInfoList.Where(gr => gr.Id != aTS_AdminLevelTypeId).ToList();
                    response.Data = aTS_AdminLevelTypeId;
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
