using Microsoft.AspNetCore.Mvc;
using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Mappers;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Lookups;
using SchedulerManagementSystem.Models.Lookups;

namespace SchedulerManagementSystem.Controllers
{
    public class EmployeeRoleController : Controller
    {
        private readonly IEmployeeRoleRepository _employeeRoleRepository;

        public EmployeeRoleController(IEmployeeRoleRepository employeeRoleRepository)
        {
            _employeeRoleRepository = employeeRoleRepository;
        }

        [HttpGet, Route("~/employee-role/")]
        public async Task<ActionResult> Index()
        {
            Response<List<EmployeeRoleInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Lookups/EmployeeRole/Index.cshtml", response);
                }

                var dbresponse = await _employeeRoleRepository.GetEmployeeRoles(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = EmployeeRoleCommonMapper.GetEmployeeRoleInfoList(dbresponse.Data)
                    };

                    return View("~/Views/Lookups/EmployeeRole/Index.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Lookups/EmployeeRole/Index.cshtml", response);
        }

        [HttpPost, Route("~/employee-role/create")]
        public async Task<IActionResult> CreateEmployeeRoleInfo(EmployeeRoleInfo employeeRoleInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (employeeRoleInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(employeeRoleInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                EmployeeRoleInfoDB employeeRoleInfoDB = EmployeeRoleCommonMapper.GetEmployeeRoleInfoDB(employeeRoleInfo);

                var dbresponse = await _employeeRoleRepository.CreateEmployeeRole(employeeRoleInfoDB, AppConstants.LOGGED_IN_USER_ID);

                if (Helpers.IsValidResponse(dbresponse))
                {
                    return StatusCode(StatusCodes.Status200OK, dbresponse);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, dbresponse);
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut, Route("~/employee-role/update")]
        public async Task<IActionResult> UpdateEmployeeRoleInfo(EmployeeRoleInfo employeeRoleInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (employeeRoleInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (!Helpers.IsValidGuid(employeeRoleInfo.Id))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(employeeRoleInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                EmployeeRoleInfoDB employeeRoleInfoDB = EmployeeRoleCommonMapper.GetEmployeeRoleInfoDB(employeeRoleInfo);

                var dbresponse = await _employeeRoleRepository.UpdateEmployeeRole(employeeRoleInfoDB, AppConstants.LOGGED_IN_USER_ID);

                if (Helpers.IsValidResponse(dbresponse))
                {
                    return StatusCode(StatusCodes.Status200OK, dbresponse);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, dbresponse);
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet, Route("~/employee-role/list")]
        public async Task<IActionResult> GetEmployeeRoleInfoList()
        {
            Response<List<EmployeeRoleInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/EmployeeRole/_EmployeeRoleList.cshtml", response);
                }

                var dbresponse = await _employeeRoleRepository.GetEmployeeRoles(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = EmployeeRoleCommonMapper.GetEmployeeRoleInfoList(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };
                    return PartialView("~/Views/Lookups/EmployeeRole/_EmployeeRoleList.cshtml", response);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;

            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/EmployeeRole/_EmployeeRoleList.cshtml", response);

        }

        [HttpGet, Route("~/employee-role/{employeeRoleInfoId:Guid}")]
        public async Task<IActionResult> GetEmployeeRoleInfoById(Guid employeeRoleInfoId)
        {
            Response<EmployeeRoleInfo> response = new Response<EmployeeRoleInfo>();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/EmployeeRole/_CreateEmployeeRole.cshtml", response);
                }

                if (!Helpers.IsValidGuid(employeeRoleInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/Lookups/EmployeeRole/_CreateEmployeeRole.cshtml", response);
                }

                var dbresponse = await _employeeRoleRepository.GetEmployeeRoleById(employeeRoleInfoId, AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = EmployeeRoleCommonMapper.GetEmployeeRoleInfo(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };

                    return PartialView("~/Views/Lookups/EmployeeRole/_CreateEmployeeRole.cshtml", response.Data);

                }

                response.Message ??= ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/EmployeeRole/_CreateEmployeeRole.cshtml", response.Data);
        }

        [HttpGet, Route("~/employee-role/data-list")]
        public async Task<IActionResult> GetEmployeeRoleInfoDataList()
        {
            Response<List<EmployeeRoleInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/Employee/Create/_EmployeeRoleList.cshtml", response);
                }

                var dbresponse = await _employeeRoleRepository.GetEmployeeRoles(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = EmployeeRoleCommonMapper.GetEmployeeRoleInfoList(dbresponse.Data),
                        Message = dbresponse.Message,
                        IsSuccess = dbresponse.IsSuccess
                    };

                    return PartialView("~/Views/Lookups/Employee/Create/_EmployeeRoleList.cshtml", response);

                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/Employee/Create/_EmployeeRoleList.cshtml", response);
        }

        [HttpDelete, Route("~/employee-role/{employeeRoleInfoId:Guid}/delete")]
        public async Task<IActionResult> DeleteEmployeeRoleInfo(Guid employeeRoleInfoId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(employeeRoleInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeRoleRepository.DeleteEmployeeRoleById(employeeRoleInfoId, AppConstants.LOGGED_IN_USER_ID);

                if (Helpers.IsValidResponse(dbresponse))
                {
                    return StatusCode(StatusCodes.Status200OK, dbresponse);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
