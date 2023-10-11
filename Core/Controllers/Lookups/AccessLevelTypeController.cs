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
    public class AccessLevelTypeController : Controller
    {
        private readonly IAccessLevelTypeRepository _accessLevelTypeRepository;

        public AccessLevelTypeController(IAccessLevelTypeRepository accessLevelTypeRepository)
        {
            _accessLevelTypeRepository = accessLevelTypeRepository;
        }

        [HttpGet, Route("~/access-level-type/")]
        public async Task<ActionResult> Index()
        {
            Response<List<AccessLevelTypeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Lookups/AccessLevelType/Index.cshtml", response);
                }

                var dbresponse = await _accessLevelTypeRepository.GetAccessLevelTypes(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = AccessLevelTypeCommonMapper.GetAccessLevelTypeInfoList(dbresponse.Data)
                    };

                    return View("~/Views/Lookups/AccessLevelType/Index.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Lookups/AccessLevelType/Index.cshtml", response);
        }

        [HttpPost, Route("~/access-level-type/create")]
        public async Task<IActionResult> CreateAccessLevelTypeInfo(AccessLevelTypeInfo accessLevelTypeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (accessLevelTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(accessLevelTypeInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                AccessLevelTypeInfoDB accessLevelTypeInfoDB = AccessLevelTypeCommonMapper.GetAccessLevelTypeInfoDB(accessLevelTypeInfo);

                var dbresponse = await _accessLevelTypeRepository.CreateAccessLevelType(accessLevelTypeInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/access-level-type/update")]
        public async Task<IActionResult> UpdateAccessLevelTypeInfo(AccessLevelTypeInfo accessLevelTypeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (accessLevelTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (!Helpers.IsValidGuid(accessLevelTypeInfo.Id))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(accessLevelTypeInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                AccessLevelTypeInfoDB accessLevelTypeInfoDB = AccessLevelTypeCommonMapper.GetAccessLevelTypeInfoDB(accessLevelTypeInfo);

                var dbresponse = await _accessLevelTypeRepository.UpdateAccessLevelType(accessLevelTypeInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpGet, Route("~/access-level-type/list")]
        public async Task<IActionResult> GetAccessLevelTypeInfoList()
        {
            Response<List<AccessLevelTypeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/AccessLevelType/_AccessLevelTypeList.cshtml", response);
                }

                var dbresponse = await _accessLevelTypeRepository.GetAccessLevelTypes(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = AccessLevelTypeCommonMapper.GetAccessLevelTypeInfoList(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };
                    return PartialView("~/Views/Lookups/AccessLevelType/_AccessLevelTypeList.cshtml", response);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;

            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/AccessLevelType/_AccessLevelTypeList.cshtml", response);

        }

        [HttpGet, Route("~/access-level-type/{accessLevelTypeInfoId:Guid}")]
        public async Task<IActionResult> GetAccessLevelTypeInfoById(Guid accessLevelTypeInfoId)
        {
            Response<AccessLevelTypeInfo> response = new Response<AccessLevelTypeInfo>();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/AccessLevelType/_CreateAccessLevelType.cshtml", response);
                }

                if (!Helpers.IsValidGuid(accessLevelTypeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/Lookups/AccessLevelType/_CreateAccessLevelType.cshtml", response);
                }

                var dbresponse = await _accessLevelTypeRepository.GetAccessLevelTypeById(accessLevelTypeInfoId, AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = AccessLevelTypeCommonMapper.GetAccessLevelTypeInfo(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };

                    return PartialView("~/Views/Lookups/AccessLevelType/_CreateAccessLevelType.cshtml", response.Data);

                }

                response.Message ??= ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/AccessLevelType/_CreateAccessLevelType.cshtml", response.Data);
        }

        [HttpGet, Route("~/access-level-type/data-list")]
        public async Task<IActionResult> GetAccessLevelTypeInfoDataList()
        {
            Response<List<AccessLevelTypeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/Employee/Create/_AccessLevelTypeList.cshtml", response);
                }

                var dbresponse = await _accessLevelTypeRepository.GetAccessLevelTypes(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = AccessLevelTypeCommonMapper.GetAccessLevelTypeInfoList(dbresponse.Data),
                        Message = dbresponse.Message,
                        IsSuccess = dbresponse.IsSuccess
                    };

                    return PartialView("~/Views/Lookups/Employee/Create/_AccessLevelTypeList.cshtml", response);

                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/Employee/Create/_AccessLevelTypeList.cshtml", response);
        }

        [HttpDelete, Route("~/access-level-type/{accessLevelTypeInfoId:Guid}/delete")]
        public async Task<IActionResult> DeleteAccessLevelTypeInfo(Guid accessLevelTypeInfoId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(accessLevelTypeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _accessLevelTypeRepository.DeleteAccessLevelTypeById(accessLevelTypeInfoId, AppConstants.LOGGED_IN_USER_ID);

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
