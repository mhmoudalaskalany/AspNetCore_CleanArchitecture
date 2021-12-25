using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackendCore.Common.DTO.Common.File;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleImpersonation;

namespace BackendCore.Common.Helpers.FileHelpers.StorageHelper
{
    public class LocalStorageService : IStorageService
    {
        private readonly ILogger<LocalStorageService> _logger;
        private readonly IConfiguration _configuration;
        public LocalStorageService(IConfiguration configuration, ILogger<LocalStorageService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// Delete Physical File
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string path)
        {
            var username = _configuration["Network:Username"];
            var password = _configuration["Network:Password"];
            var domain = _configuration["Network:Domain"];
            var credentials = new UserCredentials(domain, username, password);
            Impersonation.RunAsUser(credentials, LogonType.Interactive, () =>
            {
                File.Delete(path);
            });
            return true;
        }
        public async Task<object> DownLoad(string url, string path)
        {
            var username = _configuration["Network:Username"];
            var password = _configuration["Network:Password"];
            var domain = _configuration["Network:Domain"];
            var credentials = new UserCredentials(domain, username, password);
            var result = Impersonation.RunAsUser(credentials, LogonType.Interactive, () =>
            {
                var folderPath = Path.Combine($"{path}" + url);
                
                var memory = new MemoryStream();
                using (var stream = new FileStream(folderPath, FileMode.Open))
                {
                     stream.CopyTo(memory);
                }
                memory.Position = 0;
                return memory;
            });

            return result;


        }

        public async Task<IEnumerable<object>> StoreToSharedFolder(IFormFileCollection files, string path , string appCode)
        {
            try
            {
                var username = _configuration["Network:Username"];
                var password = _configuration["Network:Password"];
                var domain = _configuration["Network:Domain"];
                var credentials = new UserCredentials(domain, username, password);
                var result = Impersonation.RunAsUser(credentials, LogonType.Interactive, () =>
                {
                    var uploadsFolderPath = Path.Combine($"{path}") + DateTime.UtcNow.Date.ToString("dd-MM-yyyy");
                    if (!Directory.Exists(uploadsFolderPath))
                        Directory.CreateDirectory(uploadsFolderPath);
                    List<FileDto> filesName = new List<FileDto>();
                    foreach (var item in files)
                    {
                        var file = new FileDto
                        {
                            Name = item.FileName, FileSize = ((item.Length / 1024f) / 1024f).ToString(),
                            AppCode = appCode
                        };
                        var newFileName = Guid.NewGuid() + Path.GetExtension(item.FileName);
                        file.Url = newFileName;
                        file.ContentType = item.ContentType;
                        file.DocumentType = Path.GetExtension(item.FileName).Replace(".", "");
                        
                        var filePath = Path.Combine(uploadsFolderPath, newFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        filesName.Add(file);
                    }
                    return filesName;
                });
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(JsonConvert.SerializeObject(e.Message));
                Console.WriteLine(e);
                throw;
            }


        }
        
        public async Task<object> StoreBytes(UploadRequestDto dto, string path , string appCode)
        {
            try
            {
                var username = _configuration["Network:Username"];
                var password = _configuration["Network:Password"];
                var domain = _configuration["Network:Domain"];
                var credentials = new UserCredentials(domain, username, password);
                var result = Impersonation.RunAsUser(credentials, LogonType.Interactive, () =>
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
                    using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    fs.Write(dto.FileBytes, 0, dto.FileBytes.Length);
                    return file;
                });
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<IEnumerable<object>> GetDirectoriesAsync()
        {
            try
            {
                var username = _configuration["Network:Username"];
                var password = _configuration["Network:Password"];
                var domain = _configuration["Network:Domain"];
                var location = _configuration["StoragePaths:Location"];
                _logger.LogInformation("Location:  " + location);
                var credentials = new UserCredentials(domain, username, password);
                var result = Impersonation.RunAsUser(credentials, LogonType.Interactive, () => Directory.GetFiles(@location));
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(JsonConvert.SerializeObject(e.Message));
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
