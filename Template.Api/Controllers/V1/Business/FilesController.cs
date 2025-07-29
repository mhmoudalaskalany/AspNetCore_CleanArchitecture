using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.File;
using Template.Common.Core;
using Template.Common.DTO.Common.File;
using Template.Common.Extensions;
using Template.Common.Helpers.FileHelpers.Token;
using Template.Domain.Enum;

namespace Template.Api.Controllers.V1.Business
{
    /// <summary>
    /// Files Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        /// Download File With App Code From Token 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("downloadWithAppCode/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(FileResult), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<IActionResult> DownloadWithAppCodeAsync(Guid id, string token)
        {
            var result = await _fileService.DownloadWithAppCode(id, token);
            if (result.IsSuccess)
            {
                var downloadFile = (DownLoadDto)result.Data;
                return File(downloadFile.MemoryStream, downloadFile.ContentType, downloadFile.Name);
            }
            
            var errorResponse = ApiResponse<object>.ErrorResponse(result.Message, System.Net.HttpStatusCode.BadRequest, result.Errors);
            return BadRequest(errorResponse);
        }

        /// <summary>
        /// Generate Token With App Code
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        [HttpPost("generateTokenWithClaims/{appCode}")]
        [ProducesResponseType(typeof(ApiResponse<List<FileTokenDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<FileTokenDto>>), 400)]
        public ActionResult<ApiResponse<List<FileTokenDto>>> GenerateTokenWithClaimsAsync(List<Guid> ids, string appCode)
        {
            try
            {
                var secretKey = _configuration.GetValue<string>("SecurityToken:SecurityKey");
                var tokens = TokenHelper.GenerateJsonWebTokenWithClaims(60, secretKey, ids, appCode);
                var result = Result<List<FileTokenDto>>.Success(tokens, "Tokens Generated Successfully");
                return result.ToActionResult();
            }
            catch (Exception ex)
            {
                var result = Result<List<FileTokenDto>>.Failure("Failed to generate token", new[] { ex.Message });
                return result.ToActionResult();
            }
        }

        /// <summary>
        /// Upload To San Storage
        /// </summary>
        /// <param name="files"></param>
        /// <param name="storageType"></param>
        /// <param name="isPublic"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        [HttpPost("uploadToSanStorage")]
        [ProducesResponseType(typeof(ApiResponse<List<FileDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<FileDto>>), 400)]
        public async Task<ActionResult<ApiResponse<List<FileDto>>>> UploadToSanStorageAsync(IFormFileCollection files, StorageType storageType, bool isPublic, string appCode)
        {
            var result = await _fileService.UploadToSanStorage(files, storageType, isPublic, appCode);
            return result.ToActionResult();
        }

        /// <summary>
        /// Upload To Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("uploadBytes")]
        [ProducesResponseType(typeof(ApiResponse<UploadResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<UploadResponseDto>), 400)]
        public async Task<ActionResult<ApiResponse<UploadResponseDto>>> UploadBytesAsync([FromBody] UploadRequestDto dto)
        {
            var result = await _fileService.UploadBytes(dto, 10000);
            return result.ToActionResult();
        }

        /// <summary>
        /// List Current Path Directories
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getDirectories")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<ActionResult<ApiResponse<object>>> GetDirectories(StorageType storageType)
        {
            var result = await _fileService.GetDirectoriesAsync(storageType);
            return result.ToActionResult();
        }

        /// <summary>
        /// Remove by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpGet("delete/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 400)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAsync(Guid id)
        {
            var result = await _fileService.DeletePhysicalAsync(id);
            return result.ToActionResult();
        }
    }
}
