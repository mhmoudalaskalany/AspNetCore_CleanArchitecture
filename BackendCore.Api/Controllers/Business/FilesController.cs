using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Api.Controllers.Base;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Common.File;
using BackendCore.Common.Helpers.FileHelpers.Token;
using BackendCore.Entities.Enum;
using BackendCore.Service.Services.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BackendCore.Api.Controllers.Business
{
    /// <summary>
    /// Files Controller
    /// </summary>
    public class FilesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        /// <summary>
        /// Constructor
        /// </summary>
        public FilesController(IFileService fileService, IConfiguration configuration)
        {
            _fileService = fileService;
            _configuration = configuration;
        }


        /// <summary>
        /// Download File With App Code From  Token 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadWithAppCodeAsync(Guid id, string token)
        {
            var downloadFile = (DownLoadDto)await _fileService.DownloadWithAppCode(id, token);
            return File(downloadFile.MemoryStream, downloadFile.ContentType, downloadFile.Name);
        }

        /// <summary>
        /// Generate Token With App Code
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        [HttpPost("{appCode}")]
        public IFinalResult GenerateTokenWithClaimsAsync(List<Guid> ids, string appCode)
        {
            var secretKey = _configuration.GetValue<string>("SecurityToken:SecurityKey");
            var token = TokenHelper.GenerateJsonWebTokenWithClaims(60, secretKey, ids, appCode);
            return new ResponseResult(token, HttpStatusCode.OK, null, "Tokens Generated Successfully");
        }

        /// <summary>
        /// Upload To San Storage
        /// </summary>
        /// <param name="files"></param>
        /// <param name="storageType"></param>
        /// <param name="isPublic"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IFinalResult> UploadToSanStorageAsync(IFormFileCollection files, StorageType storageType, bool isPublic, string appCode)
        {
            var result = await _fileService.UploadToSanStorage(files, storageType, isPublic, appCode);
            return result;
        }

        /// <summary>
        /// Upload To Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IFinalResult> UploadBytesAsync([FromBody] UploadRequestDto dto)
        {
            var uploadResponse = await _fileService.UploadBytes(dto, 10000);
            return uploadResponse;
        }

        /// <summary>
        /// List Current Path Directories
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetDirectories(StorageType storageType)
        {
            var directories = await _fileService.GetDirectoriesAsync(storageType);
            return Ok(directories);
        }

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IFinalResult> DeleteAsync(Guid id)
        {
            return await _fileService.DeletePhysicalAsync(id);
        }

    }
}
