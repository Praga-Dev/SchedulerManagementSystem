using Microsoft.AspNetCore.Mvc;
using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Mappers;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Employee;
using SchedulerManagementSystem.Models;

namespace SchedulerManagementSystem.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet, Route("~/employee/")]
        public async Task<ActionResult> Index()
        {
            Response<List<EmployeeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Employee/Index.cshtml", response);
                }

                var dbresponse = await _employeeRepository.GetEmployees(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = EmployeeCommonMapper.GetEmployeeInfoList(dbresponse.Data)
                    };

                    return View("~/Views/Employee/Index.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Employee/Index.cshtml", response);
        }

        [HttpGet, Route("~/employee/create")]
        public async Task<ActionResult> GetCreateEmployeeView()
        {
            EmployeeInfo response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    // TODO Redirect to Login
                    return View("~/Views/Employee/CreateEmployee.cshtml", response);
                }

                return View("~/Views/Employee/CreateEmployee.cshtml", response);
            }
            catch (Exception ex)
            {
                // TODO Add exception msg to viewbag and redirect to index action
                return View("~/Views/Employee/CreateEmployee.cshtml", response);
            }
        }

        [HttpPost, Route("~/employee/create")]
        public async Task<IActionResult> CreateEmployeeInfo(EmployeeInfo employeeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (employeeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(employeeInfo.FirstName))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(employeeInfo.LastName))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                // TODO Check for all Mandatory Fields

                EmployeeInfoDB employeeInfoDB = EmployeeCommonMapper.GetEmployeeInfoDB(employeeInfo);

                var dbresponse = await _employeeRepository.CreateEmployee(employeeInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/employee/update")]
        public async Task<IActionResult> UpdateEmployeeInfo(EmployeeInfo employeeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (employeeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (!Helpers.IsValidGuid(employeeInfo.Id))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(employeeInfo.FirstName))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(employeeInfo.LastName))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                // TODO Check for all Mandatory Fields

                EmployeeInfoDB employeeInfoDB = EmployeeCommonMapper.GetEmployeeInfoDB(employeeInfo);

                var dbresponse = await _employeeRepository.UpdateEmployee(employeeInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpGet, Route("~/employee/list")]
        public async Task<IActionResult> GetEmployeeInfoList()
        {
            Response<List<EmployeeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Employee/_EmployeeList.cshtml", response);
                }

                var dbresponse = await _employeeRepository.GetEmployees(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = EmployeeCommonMapper.GetEmployeeInfoList(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };
                    return PartialView("~/Views/Employee/_EmployeeList.cshtml", response);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;

            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Employee/_EmployeeList.cshtml", response);

        }

        [HttpGet, Route("~/employee/{employeeInfoId:Guid}")]
        public async Task<IActionResult> GetEmployeeInfoById(Guid employeeInfoId)
        {
            Response<EmployeeInfo> response = new Response<EmployeeInfo>();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Employee/_CreateEmployee.cshtml", response);
                }

                if (!Helpers.IsValidGuid(employeeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/Employee/_CreateEmployee.cshtml", response);
                }

                var dbresponse = await _employeeRepository.GetEmployeeById(employeeInfoId, AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = EmployeeCommonMapper.GetEmployeeInfo(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };

                    return PartialView("~/Views/Employee/_CreateEmployee.cshtml", response.Data);

                }

                response.Message ??= ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Employee/_CreateEmployee.cshtml", response.Data);
        }

        [HttpGet, Route("~/employee/data-list")]
        public async Task<IActionResult> GetEmployeeInfoDataList()
        {
            Response<List<EmployeeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Employee/Create_EmployeeList.cshtml", response);
                }

                var dbresponse = await _employeeRepository.GetEmployees(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = EmployeeCommonMapper.GetEmployeeInfoList(dbresponse.Data),
                        Message = dbresponse.Message,
                        IsSuccess = dbresponse.IsSuccess
                    };

                    return PartialView("~/Views/Employee/Create_EmployeeList.cshtml", response);

                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Employee/Create_EmployeeList.cshtml", response);
        }

        [HttpDelete, Route("~/employee/{employeeInfoId:Guid}/delete")]
        public async Task<IActionResult> DeleteEmployeeInfo(Guid employeeInfoId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(employeeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeRepository.DeleteEmployeeById(employeeInfoId, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/employee/{employeeInfoId:Guid}/update-working-hours/")]
        public async Task<IActionResult> UpdateEmployeeInfo(Guid employeeInfoId, int employeeWorkingHours)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(employeeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeRepository.UpdateEmployeeTotalWorkingHours(employeeInfoId, employeeWorkingHours, AppConstants.LOGGED_IN_USER_ID);

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
    }
}
