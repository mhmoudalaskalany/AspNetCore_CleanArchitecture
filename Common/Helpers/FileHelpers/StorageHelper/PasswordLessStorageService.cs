using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Common.DTO.Common.File;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.Helpers.FileHelpers.StorageHelper
{
    public class PasswordLessStorageService : IStorageService
    {
        private readonly ILogger<PasswordLessStorageService> _logger;
        public PasswordLessStorageService(ILogger<PasswordLessStorageService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Delete Physical File
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string path)
        {

            File.Delete(path);

            return true;
        }
        /// <summary>
        /// Download
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<object> DownLoad(string url, string path)
        {

            var folderPath = Path.Combine($"{path}" + url);

            var memory = new MemoryStream();
            await using var stream = new FileStream(folderPath, FileMode.Open);
            await stream.CopyToAsync(memory);
            memory.Position = 0;
            return memory;



        }
        /// <summary>
        /// Store To Storage
        /// </summary>
        /// <param name="files"></param>
        /// <param name="path"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public async Task<List<FileDto>> StoreToSharedFolder(IFormFileCollection files, string path, string appCode)
        {
            try
            {


                var uploadsFolderPath = Path.Combine($"{path}") + DateTime.UtcNow.Date.ToString("dd-MM-yyyy");
                if (!Directory.Exists(uploadsFolderPath))
                    Directory.CreateDirectory(uploadsFolderPath);
                List<FileDto> filesName = new List<FileDto>();
                foreach (var item in files)
                {
                    var file = new FileDto
                    {
                        Name = item.FileName,
                        FileSize = ((item.Length / 1024f) / 1024f).ToString(),
                        AppCode = appCode
                    };
                    var newFileName = Guid.NewGuid() + Path.GetExtension(item.FileName);
                    file.Url = newFileName;
                    file.ContentType = item.ContentType;
                    file.DocumentType = Path.GetExtension(item.FileName).Replace(".", "");

                    var filePath = Path.Combine(uploadsFolderPath, newFileName);
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await item.CopyToAsync(stream);
                    filesName.Add(file);
                }
                return filesName;

            }
            catch (Exception e)
            {
                _logger.LogError(JsonConvert.SerializeObject(e.Message));
                Console.WriteLine(e);
                throw;
            }


        }
        /// <summary>
        /// Store Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="path"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public async Task<object> StoreBytes(UploadRequestDto dto, string path, string appCode)
        {
            try
            {

                var uploadsFolderPath = Path.Combine($"{path}") + DateTime.UtcNow.Date.ToString("dd-MM-yyyy");
                if (!Directory.Exists(uploadsFolderPath))
                    Directory.CreateDirectory(uploadsFolderPath);

                var file = new FileDto
                {
                    Name = dto.FileName,
                    AppCode = appCode,
                    FileSize = ((dto.FileBytes.Length / 1024f) / 1024f).ToString()
                };
                var newFileName = Guid.NewGuid() + Path.GetExtension(dto.FileName) + "." + dto.AttachmentExtension;
                file.Url = newFileName;
                file.ContentType = dto.MimeType;
                file.DocumentType = Path.GetExtension(dto.FileName).Replace(".", "");
                var filePath = Path.Combine(uploadsFolderPath, newFileName);
                await using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Write(dto.FileBytes, 0, dto.FileBytes.Length);
                return file;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        /// <summary>
        /// Get Current Folder Directories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<object>> GetDirectoriesAsync()
        {

            return new List<object>();

        }
    }
}
