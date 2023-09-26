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
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet, Route("~/location/")]
        public async Task<ActionResult> Index()
        {
            Response<List<LocationInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return View("~/Views/Location/Index.cshtml", response);
                }

                var dbresponse = await _locationRepository.GetLocations(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message,
                        Data = LocationCommonMapper.GetLocationInfoList(dbresponse.Data)
                    };

                    return View("~/Views/Location/Index.cshtml", response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return View("~/Views/Location/Index.cshtml", response);
        }

        [HttpPost, Route("~/location/create")]
        public async Task<IActionResult> CreateLocationInfo(LocationInfo locationInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (locationInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(locationInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                LocationInfoDB locationInfoDB = LocationCommonMapper.GetLocationInfoDB(locationInfo);

                var dbresponse = await _locationRepository.CreateLocation(locationInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpPut, Route("~/location/update")]
        public async Task<IActionResult> UpdateLocationInfo(LocationInfo locationInfo)
        {
            Response<Guid> response = new();

            try
            {
                if (locationInfo == null)
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (!Helpers.IsValidGuid(locationInfo.Id))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                if (string.IsNullOrWhiteSpace(locationInfo.Name))
                {
                    response.Message ??= ResponseConstants.INVALID_PARAM_GRADE_NAME;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                LocationInfoDB locationInfoDB = LocationCommonMapper.GetLocationInfoDB(locationInfo);

                var dbresponse = await _locationRepository.UpdateLocation(locationInfoDB, AppConstants.LOGGED_IN_USER_ID);

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

        [HttpGet, Route("~/location/list")]
        public async Task<IActionResult> GetLocationInfoList()
        {
            Response<List<LocationInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Location/_LocationList.cshtml", response);
                }

                var dbresponse = await _locationRepository.GetLocations(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = LocationCommonMapper.GetLocationInfoList(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };
                    return PartialView("~/Views/Location/_LocationList.cshtml", response);
                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;

            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Location/_LocationList.cshtml", response);

        }

        [HttpGet, Route("~/location/{locationInfoId:Guid}")]
        public async Task<IActionResult> GetLocationInfoById(Guid locationInfoId)
        {
            Response<LocationInfo> response = new Response<LocationInfo>();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Location/_CreateLocation.cshtml", response);
                }

                if (!Helpers.IsValidGuid(locationInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return PartialView("~/Views/Location/_CreateLocation.cshtml", response);
                }

                var dbresponse = await _locationRepository.GetLocationById(locationInfoId, AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = LocationCommonMapper.GetLocationInfo(dbresponse.Data),
                        IsSuccess = dbresponse.IsSuccess,
                        Message = dbresponse.Message
                    };

                    return PartialView("~/Views/Location/_CreateLocation.cshtml", response.Data);

                }

                response.Message ??= ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Location/_CreateLocation.cshtml", response.Data);
        }

        [HttpGet, Route("~/location/data-list")]
        public async Task<IActionResult> GetLocationInfoDataList()
        {
            Response<List<LocationInfo>> response = new();

            try
            {
                if (!Helpers.IsValidGuid(AppConstants.LOGGED_IN_USER_ID))
                {
                    response.Message ??= ResponseConstants.INVALID_LOGGED_IN_USER;
                    return PartialView("~/Views/Common/_LocationList.cshtml", response);
                }

                var dbresponse = await _locationRepository.GetLocations(AppConstants.LOGGED_IN_USER_ID);
                if (Helpers.IsValidResponse(dbresponse))
                {
                    response = new()
                    {
                        Data = LocationCommonMapper.GetLocationInfoList(dbresponse.Data),
                        Message = dbresponse.Message,
                        IsSuccess = dbresponse.IsSuccess
                    };

                    return PartialView("~/Views/Common/_LocationList.cshtml", response);

                }

                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return PartialView("~/Views/Common/_LocationList.cshtml", response);
        }

        [HttpDelete, Route("~/location/{locationInfoId:Guid}/delete")]
        public async Task<IActionResult> DeleteLocationInfo(Guid locationInfoId)
        {
            Response<Guid> response = new();

            try
            {
                if (!Helpers.IsValidGuid(locationInfoId))
                {
                    response.Message ??= ResponseConstants.INVALID_ID;
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                var dbresponse = await _locationRepository.DeleteLocationById(locationInfoId, AppConstants.LOGGED_IN_USER_ID);

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
