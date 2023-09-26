using SchedulerManagementSystem.Common.Models;

namespace SchedulerManagementSystem.Common.Utils
{
    public static class Helpers
    {
        public static bool IsValidGuid(Guid? id) => id != null && id != Guid.Empty;
        public static bool IsValidResponse<T>(Response<T> response) 
            => response != null && response.Data != null && response.IsSuccess;

        public static bool IsValidDate(DateTime dateTime)
        {
            // custom logic based on the app constraints
            return dateTime.Date > new DateTime(1900, 01, 01) && dateTime.Date < new DateTime(2050, 01, 01); 
        }
    }
}
