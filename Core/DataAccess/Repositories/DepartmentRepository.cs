using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private static List<DepartmentInfoDB> _DepartmentInfoList = new()
            {
                new() {Id = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), Name = "Department 1"},
                new() {Id = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), Name = "Department 2"},
                new() {Id = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), Name = "Department 3"},
                new() {Id = new("435EF0D5-D5CC-4CD6-969A-419200678422"), Name = "Department 4"},
            };


        //public readonly List<DepartmentInfoDB> DepartmentInfoList = _DepartmentInfoList;
        public async Task<Response<Guid>> CreateDepartment(DepartmentInfoDB departmentInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (departmentInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    departmentInfoDB.Id = id;
                    _DepartmentInfoList.Add(departmentInfoDB);

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

        public async Task<Response<Guid>> UpdateDepartment(DepartmentInfoDB departmentInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (departmentInfoDB != null && Helpers.IsValidGuid(departmentInfoDB.Id))
                {
                    DepartmentInfoDB? department = _DepartmentInfoList.FirstOrDefault(gr => gr.Id == departmentInfoDB.Id);
                    if (department != null)
                    {
                        department.Name = departmentInfoDB.Name;

                        response.Data = departmentInfoDB.Id;
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

        public async Task<Response<List<DepartmentInfoDB>>> GetDepartments(Guid loggedInUserId)
        {
            return new()
            {
                Data = _DepartmentInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<DepartmentInfoDB>> GetDepartmentById(Guid departmentId, Guid loggedInUserId)
        {
            Response<DepartmentInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(departmentId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = _DepartmentInfoList.FirstOrDefault(gr => gr.Id == departmentId) ?? new()
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

        public async Task<Response<Guid>> DeleteDepartmentById(Guid departmentId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(departmentId))
                {
                    _DepartmentInfoList = _DepartmentInfoList.Where(gr => gr.Id != departmentId).ToList();
                    response.Data = departmentId;
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
