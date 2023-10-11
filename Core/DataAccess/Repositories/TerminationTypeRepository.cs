using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class TerminationTypeRepository : ITerminationTypeRepository
    {
        private static List<TerminationTypeInfoDB> _TerminationTypeInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "TerminationType 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "TerminationType 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "TerminationType 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "TerminationType 4"},
            };


        //public readonly List<TerminationTypeInfoDB> TerminationTypeInfoList = _TerminationTypeInfoList;
        public async Task<Response<Guid>> CreateTerminationType(TerminationTypeInfoDB terminationTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (terminationTypeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    terminationTypeInfoDB.Id = id;
                    _TerminationTypeInfoList.Add(terminationTypeInfoDB);

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

        public async Task<Response<Guid>> UpdateTerminationType(TerminationTypeInfoDB terminationTypeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (terminationTypeInfoDB != null && Helpers.IsValidGuid(terminationTypeInfoDB.Id))
                {
                    TerminationTypeInfoDB? terminationType = _TerminationTypeInfoList.FirstOrDefault(gr => gr.Id == terminationTypeInfoDB.Id);
                    if (terminationType != null)
                    {
                        terminationType.Name = terminationTypeInfoDB.Name;

                        response.Data = terminationTypeInfoDB.Id;
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

        public async Task<Response<List<TerminationTypeInfoDB>>> GetTerminationTypes(Guid loggedInUserId)
        {
            return new()
            {
                Data = _TerminationTypeInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<TerminationTypeInfoDB>> GetTerminationTypeById(Guid terminationTypeId, Guid loggedInUserId)
        {
            Response<TerminationTypeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(terminationTypeId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _TerminationTypeInfoList.FirstOrDefault(gr => gr.Id == terminationTypeId) ?? new()
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

        public async Task<Response<Guid>> DeleteTerminationTypeById(Guid terminationTypeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(terminationTypeId))
                {
                    _TerminationTypeInfoList = _TerminationTypeInfoList.Where(gr => gr.Id != terminationTypeId).ToList();
                    response.Data = terminationTypeId;
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
