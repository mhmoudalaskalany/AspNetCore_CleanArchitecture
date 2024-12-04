using Hangfire.Dashboard;

namespace Template.Api.Authorization
{
    /// <summary>
    /// Hang Fire
    /// </summary>
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Authorize
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
