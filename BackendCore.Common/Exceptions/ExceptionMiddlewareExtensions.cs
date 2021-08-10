using Microsoft.AspNetCore.Builder;

namespace BackendCore.Common.Exceptions
{
   public static class ExceptionMiddlewareExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
