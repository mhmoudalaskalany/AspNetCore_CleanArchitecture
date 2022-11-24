using System;
using System.IO;
using Common.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Common.Helpers.MediaUploader
{
    public class UploaderConfiguration : IUploaderConfiguration
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UploaderConfiguration(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public string SaveBase64(string fileBase64, string fileName, string folderName, string oldFileName = null)
        {
            try
            {
                var path = $"{_hostingEnvironment.ContentRootPath}/{folderName}";
                if (!string.IsNullOrWhiteSpace(oldFileName)) RemoveFile(oldFileName, folderName);
                if (!fileBase64.Contains("base64")) return null;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var (fileExtension, data) = fileBase64.GetBase64StringContents();
                var fileFullName = $"{fileName}-{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(path, fileFullName);
                var fileBytes = Convert.FromBase64String(data);
                File.WriteAllBytes(filePath, fileBytes);
                return fileFullName;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public string ConvertToBase64String(string fileName, string folderName)
        {
            var path = $"{_hostingEnvironment.ContentRootPath}/{folderName}/{fileName}";
            if (!File.Exists(path)) path = $"{_hostingEnvironment.ContentRootPath}/StaticFiles/No-Image-Available.png";
            var fileByte = File.ReadAllBytes(path);
            Stream stream = new MemoryStream(fileByte);
            var imgExtension = path.Contains("StaticFiles/No-Image-Available.png") ? ".png" : fileName.Split('.')[1];
            return $"data:{MimeTypeMap.GetMimeType(imgExtension)};base64,{Convert.ToBase64String(fileByte)}";
        }
        public Stream ConvertToStream(string fileName, string folderName)
        {
            var path = $"{_hostingEnvironment.ContentRootPath}/{folderName}/{fileName}";
            if (!File.Exists(path)) path = $"{_hostingEnvironment.ContentRootPath}/StaticFiles/No-Image-Available.png";
            var fileByte = File.ReadAllBytes(path);
            return new MemoryStream(fileByte);
        }
        public void RemoveFile(string fileName, string folderName)
        {
            var fullPath = $"{_hostingEnvironment.ContentRootPath}/{folderName}/{fileName}";
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
    }
}
