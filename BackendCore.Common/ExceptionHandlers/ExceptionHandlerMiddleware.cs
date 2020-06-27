using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BackendCore.Common.ExceptionHandlers
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context , IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                Log(context, e , hostingEnvironment);
                throw;
            }
            
        }

        private void Log(HttpContext context, Exception exception , IWebHostEnvironment hostingEnvironment)
        {
            var savePath = hostingEnvironment.WebRootPath;
            var now = DateTime.UtcNow;
            var fileName = $"{now.ToString("yyyy_MM_dd")}.log";
            var filePath = Path.Combine(savePath, "logs", fileName);

            // ensure that directory exists
            new FileInfo(filePath).Directory.Create();

            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine($"{now.ToString("HH:mm:ss")} {context.Request.Path}");
                writer.WriteLine(exception.Message);
            }
        }
    }
}
