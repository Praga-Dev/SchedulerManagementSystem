using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class EmployeeRoleRepository : IEmployeeRoleRepository
    {
        private static List<EmployeeRoleInfoDB> _EmployeeRoleInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "EmployeeRole 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "EmployeeRole 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "EmployeeRole 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "EmployeeRole 4"},
            };


        //public readonly List<EmployeeRoleInfoDB> EmployeeRoleInfoList = _EmployeeRoleInfoList;
        public async Task<Response<Guid>> CreateEmployeeRole(EmployeeRoleInfoDB employeeRoleInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (employeeRoleInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    employeeRoleInfoDB.Id = id;
                    _EmployeeRoleInfoList.Add(employeeRoleInfoDB);

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

        public async Task<Response<Guid>> UpdateEmployeeRole(EmployeeRoleInfoDB employeeRoleInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (employeeRoleInfoDB != null && Helpers.IsValidGuid(employeeRoleInfoDB.Id))
                {
                    EmployeeRoleInfoDB? employeeRole = _EmployeeRoleInfoList.FirstOrDefault(gr => gr.Id == employeeRoleInfoDB.Id);
                    if (employeeRole != null)
                    {
                        employeeRole.Name = employeeRoleInfoDB.Name;

                        response.Data = employeeRoleInfoDB.Id;
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

        public async Task<Response<List<EmployeeRoleInfoDB>>> GetEmployeeRoles(Guid loggedInUserId)
        {
            return new()
            {
                Data = _EmployeeRoleInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<EmployeeRoleInfoDB>> GetEmployeeRoleById(Guid employeeRoleId, Guid loggedInUserId)
        {
            Response<EmployeeRoleInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(employeeRoleId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _EmployeeRoleInfoList.FirstOrDefault(gr => gr.Id == employeeRoleId) ?? new()
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

        public async Task<Response<Guid>> DeleteEmployeeRoleById(Guid employeeRoleId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(employeeRoleId))
                {
                    _EmployeeRoleInfoList = _EmployeeRoleInfoList.Where(gr => gr.Id != employeeRoleId).ToList();
                    response.Data = employeeRoleId;
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
