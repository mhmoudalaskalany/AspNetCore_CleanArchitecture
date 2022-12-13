using System;
using System.Threading.Tasks;
using Template.Common.Core;
using Microsoft.AspNetCore.Http;
using Template.Application.Services.Base;
using Template.Common.DTO.Common.File;
using Template.Domain.Enum;

namespace Template.Application.Services.File
{
    public interface IFileService : IBaseService<Domain.Entities.Business.File, AddFileDto, FileDto, Guid, Guid?>
    {

        /// <summary>
        /// Upload To Shared Storage
        /// </summary>
        /// <param name="files"></param>
        /// <param name="storageType"></param>
        /// <param name="isPublic"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        Task<IFinalResult> UploadToSanStorage(IFormFileCollection files, StorageType storageType, bool isPublic, string appCode);
        /// <summary>
        /// Upload Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<IFinalResult> UploadBytes(UploadRequestDto dto, int length);
        /// <summary>
        /// Download With App Code
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<object> DownloadWithAppCode(Guid id, string token);
        /// <summary>
        /// Get Directories
        /// </summary>
        /// <param name="storageType"></param>
        /// <returns></returns>
        Task<object> GetDirectoriesAsync(StorageType storageType);
        /// <summary>
        /// Delete File Physical from database and folder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IFinalResult> DeletePhysicalAsync(Guid id);
    }
}