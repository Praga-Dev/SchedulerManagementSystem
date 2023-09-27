using Microsoft.AspNetCore.Mvc;
using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Mappers;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels;
using SchedulerManagementSystem.Models.Scheduler;

namespace SchedulerManagementSystem.Controllers
{
    public class EmployeeSchedulerController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeSchedulerRepository _employeeSchedulerRepository;
        public EmployeeSchedulerController(IEmployeeRepository employeeRepository, IEmployeeSchedulerRepository employeeSchedulerRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeSchedulerRepository = employeeSchedulerRepository;
        }

        [HttpGet, Route("~/employee-scheduler")]
        public async Task<ActionResult> Index()
        {
            Response<DashboardViewModel> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/EmployeeScheduler/Index.cshtml", response);
                }

                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddMonths(3);

                response = await _GetEmployeeSchedulerCalendar(startDate, endDate);
                return View("~/Views/EmployeeScheduler/Index.cshtml", response);
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return View("~/Views/EmployeeScheduler/Index.cshtml", response);
            }
        }

        [HttpGet, Route("~/employee-scheduler/data")]
        public async Task<ActionResult> GetCurrentQuarterJSON()
        {
            Response<DashboardViewModel> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return Json(response);
                }

                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddMonths(3);

                response = await _GetEmployeeSchedulerCalendar(startDate, endDate);
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return Json(response);
            }
        }



        [HttpGet, Route("~/employee-scheduler/previous-quarter/")]
        public async Task<ActionResult> GetPreviousQuarterOnCalendar(string endDateStr)
        {
            Response<DashboardViewModel> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
                }

                DateTime endDate = Convert.ToDateTime(endDateStr).AddDays(-1);

                if (!Helpers.IsValidDate(endDate))
                {
                    response.Message ??= ResponseConstants.INVALID_DATE;
                    return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
                }

                DateTime startDate = endDate.AddMonths(-3);

                response = await _GetEmployeeSchedulerCalendar(startDate, endDate);
                return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
            }
        }

        [HttpGet, Route("~/employee-scheduler/next-quarter/")]
        public async Task<ActionResult> GetNextQuarterOnCalendar(string startDateStr)
        {
            Response<DashboardViewModel> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
                }

                DateTime startDate = Convert.ToDateTime(startDateStr).AddDays(1);

                if (!Helpers.IsValidDate(startDate))
                {
                    response.Message ??= ResponseConstants.INVALID_DATE;
                    return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
                }

                DateTime endDate = startDate.AddMonths(3);

                response = await _GetEmployeeSchedulerCalendar(startDate, endDate);
                return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return PartialView("~/Views/EmployeeScheduler/_EmployeeSchedulerCalendar.cshtml", response);
            }
        }

        [HttpPut, Route("~/employee-scheduler/{employeeSchedulerId:Guid}/update-allocation-hour/{updateAllocationHour:int}")]
        public async Task<IActionResult> UpdateAllocationWorkingHour(Guid employeeSchedulerId, int updateAllocationHour)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(employeeSchedulerId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (updateAllocationHour < 0)
                {
                    response.Message ??= ResponseConstants.INVALID_ALLOCATION_HOUR;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeSchedulerRepository.UpdateAllocationWorkingHour(employeeSchedulerId, updateAllocationHour, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpGet, Route("~/employee-scheduler/manage-calendar-employee")]
        public async Task<ActionResult> GetManageCalendarEmployeeList()
        {
            Response<List<CalendarEmployeeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/EmployeeScheduler/_ManageCalendarEmployeeList.cshtml", response);
                }

                var dbresponse = await _employeeSchedulerRepository.GetManageCalendarEmployeeList(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = EmployeeSchedulerCommonMapper.GetCalendarEmployeeInfoList(dbresponse.Data)
                    };

                    return PartialView("~/Views/EmployeeScheduler/_ManageCalendarEmployeeList.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/EmployeeScheduler/_ManageCalendarEmployeeList.cshtml", response);
        }

        [HttpPut, Route("~/employee-scheduler/manage-calendar-employee/{employeeId:Guid}/add")]
        public async Task<IActionResult> AddEmployeeFromCalendar(Guid employeeId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(employeeId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeSchedulerRepository.AddEmployeeFromCalendar(employeeId, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/employee-scheduler/manage-calendar-employee/{employeeId:Guid}/remove")]
        public async Task<IActionResult> RemoveEmployeeFromCalendar(Guid employeeId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(employeeId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeSchedulerRepository.RemoveEmployeeFromCalendar(employeeId, AppConstants.LOGGED_IN_USER_ID);

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

        // Replace Employee
        [HttpGet, Route("~/employee-scheduler/{employeeId:Guid}/replace")]
        public async Task<ActionResult> GetReplaceEmployeeCalendarList(Guid employeeId)
        {
            Response<List<CalendarEmployeeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/EmployeeScheduler/_ReplaceEmployee.cshtml", response);
                }

                if (!Helpers.IsValidGuid(employeeId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/EmployeeScheduler/_ReplaceEmployee.cshtml", response);
                }

                var empResponse = await _employeeRepository.GetEmployeeById(employeeId, AppConstants.LOGGED_IN_USER_ID);
                if (!Helpers.IsValidResponse(empResponse))
                {
                    response.Message ??= ResponseConstants.NO_RECORDS_FOUND;
                    return PartialView("~/Views/EmployeeScheduler/_ReplaceEmployee.cshtml", response);
                }

                var dbresponse = await _employeeSchedulerRepository.GetManageCalendarEmployeeList(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = EmployeeSchedulerCommonMapper.GetCalendarEmployeeInfoList(dbresponse.Data)
                    };

                    // Need in View
                    TempData["OLD_EMPLOYEE"] = employeeId;

                    return PartialView("~/Views/EmployeeScheduler/_ReplaceEmployee.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/EmployeeScheduler/_ReplaceEmployee.cshtml", response);
        }

        [HttpPut, Route("~/employee-scheduler/{oldEmployeeId:Guid}/replace/{newEmployeeId:Guid}/")]
        public async Task<IActionResult> ReplaceEmployeeFromCalendar(Guid oldEmployeeId, Guid newEmployeeId)
        {
            Response<Guid> response = new();

            try
            {
                if (!(Helpers.IsValidGuid(oldEmployeeId) && Helpers.IsValidGuid(oldEmployeeId)))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeSchedulerRepository.ReplaceEmployeeFromCalendar(oldEmployeeId, newEmployeeId, AppConstants.LOGGED_IN_USER_ID);

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

        //Bulk operations

        [HttpPut, Route("~/employee-scheduler/manage-calendar-employee/bulk/add")]
        public async Task<IActionResult> AddBulkEmployeesFromCalendar(List<Guid> employeeIdList)
        {
            Response<Guid> response = new();

            try
            {
                if (employeeIdList.Count <= 0)
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeSchedulerRepository.AddBulkEmployeesFromCalendar(employeeIdList, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/employee-scheduler/manage-calendar-employee/bulk/remove")]
        public async Task<IActionResult> RemoveBulkEmployeesFromCalendar(List<Guid> employeeIdList)
        {
            Response<Guid> response = new();

            try
            {
                if (employeeIdList.Count <= 0)
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _employeeSchedulerRepository.RemoveBulkEmployeesFromCalendar(employeeIdList, AppConstants.LOGGED_IN_USER_ID);

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

        

        private async Task<Response<DashboardViewModel>> _GetEmployeeSchedulerCalendar(DateTime startDate, DateTime endDate)
        {
            Response<DashboardViewModel> response = new();

            try
            {
                var employeeSchedulerResponse = await _employeeSchedulerRepository.GetEmployeeSchedulerCalendar(startDate, endDate, AppConstants.LOGGED_IN_USER_ID);
                if (!Helpers.IsValidResponse(employeeSchedulerResponse))
                {
                    response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
                    return response;
                }


                var emplListResponse = await _employeeRepository.GetEmployees(AppConstants.LOGGED_IN_USER_ID);
                if (!Helpers.IsValidResponse(emplListResponse))
                {
                    response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
                    return response;
                }

                // TODO move to repository method
                List<Guid> includeEmployeeIDs = new(employeeSchedulerResponse.Data.Select(empSch => empSch.EmployeeInfoId));
                List<EmployeeInfoDB> empListData = emplListResponse.Data.Where(
                    emp => includeEmployeeIDs.Contains(emp.Id)).ToList();

                var employeeInfoList = EmployeeCommonMapper.GetEmployeeInfoList(empListData);
                var employeeSchedulerInfoList = EmployeeSchedulerCommonMapper.GetEmployeeSchedulerInfoList(employeeSchedulerResponse.Data);

                response.Data = new()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    EmployeeSchedulerVMList = EmployeeSchedulerCommonMapper.GetEmployeeSchedulerVM(employeeSchedulerInfoList, employeeInfoList)
                };

                response.IsSuccess = true;
                response.Message = ResponseConstants.SUCCESS;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
                return response;
            }
        }
    }
}
