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
    public class ATS_AdminLevelTypeController : Controller
    {
        private readonly IATS_AdminLevelTypeRepository _aTS_AdminLevelTypeRepository;

        public ATS_AdminLevelTypeController(IATS_AdminLevelTypeRepository aTS_AdminLevelTypeRepository)
        {
            _aTS_AdminLevelTypeRepository = aTS_AdminLevelTypeRepository;
        }

        [HttpGet, Route("~/ats-admin-level-type/")]
        public async Task<ActionResult> Index()
        {
            Response<List<ATS_AdminLevelTypeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Lookups/ATS_AdminLevelType/Index.cshtml", response);
                }

                var dbresponse = await _aTS_AdminLevelTypeRepository.GetATS_AdminLevelTypes(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = ATS_AdminLevelTypeCommonMapper.GetATS_AdminLevelTypeInfoList(dbresponse.Data)
                    };

                    return View("~/Views/Lookups/ATS_AdminLevelType/Index.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Lookups/ATS_AdminLevelType/Index.cshtml", response);
        }

        [HttpPost, Route("~/ats-admin-level-type/create")]
        public async Task<IActionResult> CreateATS_AdminLevelTypeInfo(ATS_AdminLevelTypeInfo aTS_AdminLevelTypeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (aTS_AdminLevelTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(aTS_AdminLevelTypeInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                ATS_AdminLevelTypeInfoDB aTS_AdminLevelTypeInfoDB = ATS_AdminLevelTypeCommonMapper.GetATS_AdminLevelTypeInfoDB(aTS_AdminLevelTypeInfo);

                var dbresponse = await _aTS_AdminLevelTypeRepository.CreateATS_AdminLevelType(aTS_AdminLevelTypeInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/ats-admin-level-type/update")]
        public async Task<IActionResult> UpdateATS_AdminLevelTypeInfo(ATS_AdminLevelTypeInfo aTS_AdminLevelTypeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (aTS_AdminLevelTypeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (!Helpers.IsValidGuid(aTS_AdminLevelTypeInfo.Id))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(aTS_AdminLevelTypeInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                ATS_AdminLevelTypeInfoDB aTS_AdminLevelTypeInfoDB = ATS_AdminLevelTypeCommonMapper.GetATS_AdminLevelTypeInfoDB(aTS_AdminLevelTypeInfo);

                var dbresponse = await _aTS_AdminLevelTypeRepository.UpdateATS_AdminLevelType(aTS_AdminLevelTypeInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpGet, Route("~/ats-admin-level-type/list")]
        public async Task<IActionResult> GetATS_AdminLevelTypeInfoList()
        {
            Response<List<ATS_AdminLevelTypeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/ATS_AdminLevelType/_ATS_AdminLevelTypeList.cshtml", response);
                }

                var dbresponse = await _aTS_AdminLevelTypeRepository.GetATS_AdminLevelTypes(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = ATS_AdminLevelTypeCommonMapper.GetATS_AdminLevelTypeInfoList(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };
                    return PartialView("~/Views/Lookups/ATS_AdminLevelType/_ATS_AdminLevelTypeList.cshtml", response);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;

            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/ATS_AdminLevelType/_ATS_AdminLevelTypeList.cshtml", response);

        }

        [HttpGet, Route("~/ats-admin-level-type/{aTS_AdminLevelTypeInfoId:Guid}")]
        public async Task<IActionResult> GetATS_AdminLevelTypeInfoById(Guid aTS_AdminLevelTypeInfoId)
        {
            Response<ATS_AdminLevelTypeInfo> response = new Response<ATS_AdminLevelTypeInfo>();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/ATS_AdminLevelType/_CreateATS_AdminLevelType.cshtml", response);
                }

                if (!Helpers.IsValidGuid(aTS_AdminLevelTypeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/Lookups/ATS_AdminLevelType/_CreateATS_AdminLevelType.cshtml", response);
                }

                var dbresponse = await _aTS_AdminLevelTypeRepository.GetATS_AdminLevelTypeById(aTS_AdminLevelTypeInfoId, AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = ATS_AdminLevelTypeCommonMapper.GetATS_AdminLevelTypeInfo(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };

                    return PartialView("~/Views/Lookups/ATS_AdminLevelType/_CreateATS_AdminLevelType.cshtml", response.Data);

                }

                response.Message ??= ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/ATS_AdminLevelType/_CreateATS_AdminLevelType.cshtml", response.Data);
        }

        [HttpGet, Route("~/ats-admin-level-type/data-list")]
        public async Task<IActionResult> GetATS_AdminLevelTypeInfoDataList()
        {
            Response<List<ATS_AdminLevelTypeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/Employee/Create/_ATS_AdminLevelTypeList.cshtml", response);
                }

                var dbresponse = await _aTS_AdminLevelTypeRepository.GetATS_AdminLevelTypes(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = ATS_AdminLevelTypeCommonMapper.GetATS_AdminLevelTypeInfoList(dbresponse.Data),
                        Message = dbresponse.Message,
                        IsSuccess = dbresponse.IsSuccess
                    };

                    return PartialView("~/Views/Lookups/Employee/Create/_ATS_AdminLevelTypeList.cshtml", response);

                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/Employee/Create/_ATS_AdminLevelTypeList.cshtml", response);
        }

        [HttpDelete, Route("~/ats-admin-level-type/{aTS_AdminLevelTypeInfoId:Guid}/delete")]
        public async Task<IActionResult> DeleteATS_AdminLevelTypeInfo(Guid aTS_AdminLevelTypeInfoId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(aTS_AdminLevelTypeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _aTS_AdminLevelTypeRepository.DeleteATS_AdminLevelTypeById(aTS_AdminLevelTypeInfoId, AppConstants.LOGGED_IN_USER_ID);

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
