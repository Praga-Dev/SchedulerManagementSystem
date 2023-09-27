using Microsoft.AspNetCore.Mvc;
using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Mappers;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels;
using SchedulerManagementSystem.Models;

namespace SchedulerManagementSystem.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        [HttpGet, Route("~/grade/")]
        public async Task<ActionResult> Index()
        {
            Response<List<GradeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Grade/Index.cshtml", response);
                }

                var dbresponse = await _gradeRepository.GetGrades(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = GradeCommonMapper.GetGradeInfoList(dbresponse.Data)
                    };

                    return View("~/Views/Grade/Index.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Grade/Index.cshtml", response);
        }

        [HttpPost, Route("~/grade/create")]
        public async Task<IActionResult> CreateGradeInfo(GradeInfo gradeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (gradeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(gradeInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                GradeInfoDB gradeInfoDB = GradeCommonMapper.GetGradeInfoDB(gradeInfo);

                var dbresponse = await _gradeRepository.CreateGrade(gradeInfoDB, AppConstants.LOGGED_IN_USER_ID);
                
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

        [HttpPut, Route("~/grade/update")]
        public async Task<IActionResult> UpdateGradeInfo(GradeInfo gradeInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (gradeInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (!Helpers.IsValidGuid(gradeInfo.Id))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(gradeInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                GradeInfoDB gradeInfoDB = GradeCommonMapper.GetGradeInfoDB(gradeInfo);

                var dbresponse = await _gradeRepository.UpdateGrade(gradeInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpGet, Route("~/grade/list")]
        public async Task<IActionResult> GetGradeInfoList()
        {
            Response<List<GradeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Grade/_GradeList.cshtml", response);
                }

                var dbresponse = await _gradeRepository.GetGrades(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = GradeCommonMapper.GetGradeInfoList(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };
                    return PartialView("~/Views/Grade/_GradeList.cshtml", response);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;

            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Grade/_GradeList.cshtml", response);

        }

        [HttpGet, Route("~/grade/{gradeInfoId:Guid}")]
        public async Task<IActionResult> GetGradeInfoById(Guid gradeInfoId)
        {
            Response<GradeInfo> response = new Response<GradeInfo>();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Grade/_CreateGrade.cshtml", response);
                }

                if (!Helpers.IsValidGuid(gradeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/Grade/_CreateGrade.cshtml", response);
                }

                var dbresponse = await _gradeRepository.GetGradeById(gradeInfoId, AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = GradeCommonMapper.GetGradeInfo(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };

                    return PartialView("~/Views/Grade/_CreateGrade.cshtml", response.Data);

                }

                response.Message ??= ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Grade/_CreateGrade.cshtml", response.Data);
        }

        [HttpGet, Route("~/grade/data-list")]
        public async Task<IActionResult> GetGradeInfoDataList()
        {
            Response<List<GradeInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_GradeList.cshtml", response);
                }

                var dbresponse = await _gradeRepository.GetGrades(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = GradeCommonMapper.GetGradeInfoList(dbresponse.Data),
                        Message = dbresponse.Message,
                        IsSuccess = dbresponse.IsSuccess
                    };

                    return PartialView("~/Views/Common/_GradeList.cshtml", response);

                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_GradeList.cshtml", response);
        }

        [HttpDelete, Route("~/grade/{gradeInfoId:Guid}/delete")]
        public async Task<IActionResult> DeleteGradeInfo(Guid gradeInfoId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(gradeInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _gradeRepository.DeleteGradeById(gradeInfoId, AppConstants.LOGGED_IN_USER_ID);

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
