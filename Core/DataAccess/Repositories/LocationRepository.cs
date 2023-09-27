using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public List<LocationInfoDB> LocationInfoList { get; set; }

        public LocationRepository()
        {
            LocationInfoList = new()
            {
                new() {Id = new("455765AA-03A5-40C0-9997-242470DFA9AE"), Name = "India"},
                new() {Id = new("FC438154-45A3-4FD2-A150-354803DB8B2C"), Name = "Ireland"},
                new() {Id = new("9CED8A68-EEC6-467D-8625-8FA54379DE26"), Name = "United Kingdom"},
                new() {Id = new("0477fd73-289b-4ef0-a175-29e5691b26c7"), Name = "United States"},
            };

        }

        public async Task<Response<Guid>> CreateLocation(LocationInfoDB locationInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (locationInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    locationInfoDB.Id = id;
                    LocationInfoList.Add(locationInfoDB);

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

        public async Task<Response<Guid>> UpdateLocation(LocationInfoDB locationInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (locationInfoDB != null && Helpers.IsValidGuid(locationInfoDB.Id))
                {
                    LocationInfoDB? location = LocationInfoList.FirstOrDefault(gr => gr.Id == locationInfoDB.Id);
                    if (location != null)
                    {
                        location.Name = locationInfoDB.Name;

                        response.Data = locationInfoDB.Id;
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

        public async Task<Response<List<LocationInfoDB>>> GetLocations(Guid loggedInUserId)
        {
            return new()
            {
                Data = LocationInfoList,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<LocationInfoDB>> GetLocationById(Guid locationId, Guid loggedInUserId)
        {
            Response<LocationInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(locationId))
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = ResponseConstants.SUCCESS,
                        Data = LocationInfoList.FirstOrDefault(gr => gr.Id == locationId) ?? new()
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

        public async Task<Response<Guid>> DeleteLocationById(Guid locationId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(locationId))
                {
                    LocationInfoList = LocationInfoList.Where(gr => gr.Id != locationId).ToList();
                    response.Data = locationId;
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
