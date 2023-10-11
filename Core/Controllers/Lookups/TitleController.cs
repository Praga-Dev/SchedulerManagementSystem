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
    public class TitleController : Controller
    {
        private readonly ITitleRepository _titleRepository;

        public TitleController(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        [HttpGet, Route("~/title/")]
        public async Task<ActionResult> Index()
        {
            Response<List<TitleInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Lookups/Title/Index.cshtml", response);
                }

                var dbresponse = await _titleRepository.GetTitles(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = TitleCommonMapper.GetTitleInfoList(dbresponse.Data)
                    };

                    return View("~/Views/Lookups/Title/Index.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Lookups/Title/Index.cshtml", response);
        }

        [HttpPost, Route("~/title/create")]
        public async Task<IActionResult> CreateTitleInfo(TitleInfo titleInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (titleInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(titleInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                TitleInfoDB titleInfoDB = TitleCommonMapper.GetTitleInfoDB(titleInfo);

                var dbresponse = await _titleRepository.CreateTitle(titleInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/title/update")]
        public async Task<IActionResult> UpdateTitleInfo(TitleInfo titleInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (titleInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (!Helpers.IsValidGuid(titleInfo.Id))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(titleInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                TitleInfoDB titleInfoDB = TitleCommonMapper.GetTitleInfoDB(titleInfo);

                var dbresponse = await _titleRepository.UpdateTitle(titleInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpGet, Route("~/title/list")]
        public async Task<IActionResult> GetTitleInfoList()
        {
            Response<List<TitleInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/Title/_TitleList.cshtml", response);
                }

                var dbresponse = await _titleRepository.GetTitles(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = TitleCommonMapper.GetTitleInfoList(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };
                    return PartialView("~/Views/Lookups/Title/_TitleList.cshtml", response);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;

            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/Title/_TitleList.cshtml", response);

        }

        [HttpGet, Route("~/title/{titleInfoId:Guid}")]
        public async Task<IActionResult> GetTitleInfoById(Guid titleInfoId)
        {
            Response<TitleInfo> response = new Response<TitleInfo>();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/Title/_CreateTitle.cshtml", response);
                }

                if (!Helpers.IsValidGuid(titleInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/Lookups/Title/_CreateTitle.cshtml", response);
                }

                var dbresponse = await _titleRepository.GetTitleById(titleInfoId, AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = TitleCommonMapper.GetTitleInfo(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };

                    return PartialView("~/Views/Lookups/Title/_CreateTitle.cshtml", response.Data);

                }

                response.Message ??= ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/Title/_CreateTitle.cshtml", response.Data);
        }

        [HttpGet, Route("~/title/data-list")]
        public async Task<IActionResult> GetTitleInfoDataList()
        {
            Response<List<TitleInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Lookups/Employee/Create/_TitleList.cshtml", response);
                }

                var dbresponse = await _titleRepository.GetTitles(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = TitleCommonMapper.GetTitleInfoList(dbresponse.Data),
                        Message = dbresponse.Message,
                        IsSuccess = dbresponse.IsSuccess
                    };

                    return PartialView("~/Views/Lookups/Employee/Create/_TitleList.cshtml", response);

                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Lookups/Employee/Create/_TitleList.cshtml", response);
        }

        [HttpDelete, Route("~/title/{titleInfoId:Guid}/delete")]
        public async Task<IActionResult> DeleteTitleInfo(Guid titleInfoId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(titleInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _titleRepository.DeleteTitleById(titleInfoId, AppConstants.LOGGED_IN_USER_ID);

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
