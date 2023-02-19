using System;
using System.Threading.Tasks;
using Template.Common.Core;
using Microsoft.AspNetCore.Http;
using Template.Application.Services.Base;
using Template.Common.DTO.Common.File;
using Template.Domain.Enum;

namespace Template.Application.Services.File
{
    public interface IFileService : IBaseService<Domain.Entities.Business.File, AddFileDto , EditFileDto, FileDto, Guid, Guid?>
    {

        /// <summary>
        /// Upload To Shared Storage
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> UploadToSanStorage(IFormFileCollection files, StorageType storageType, bool isPublic, string appCode);

        /// <summary>
        /// Upload Bytes
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> UploadBytes(UploadRequestDto dto, int length);

        /// <summary>
        /// Download With App Code
        /// </summary>
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
        /// <returns></returns>
        Task<IFinalResult> DeletePhysicalAsync(Guid id);

    }
}