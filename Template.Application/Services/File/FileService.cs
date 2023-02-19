using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.Helpers.FileHelpers.Crypto;
using Template.Common.Helpers.FileHelpers.StorageHelper;
using Template.Common.Helpers.FileHelpers.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Template.Application.Services.Base;
using Template.Common.DTO.Common.File;
using Template.Domain.Enum;

namespace Template.Application.Services.File
{
    public class FileService : BaseService<Domain.Entities.Business.File, AddFileDto , EditFileDto, FileDto, Guid, Guid?>, IFileService
    {
        private readonly Func<string, IStorageService> _storage;
        private readonly IConfiguration _configuration;
        private string _path;

        public FileService(IServiceBaseParameter<Domain.Entities.Business.File> parameters, IConfiguration configuration, Func<string, IStorageService> storage) : base(parameters)
        {
            _configuration = configuration;
            _storage = storage;
        }


        /// <summary>
        /// Upload To Shared Storage
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> UploadToSanStorage(IFormFileCollection files, StorageType storageType, bool isPublic, string appCode)
        {
          
            var basePath = _configuration["StoragePaths:Base"];
            _path = basePath + _configuration["StoragePaths:" + appCode];
            var fileNames = await _storage(storageType.ToString()).StoreToSharedFolder(files, _path, appCode);
            fileNames.ForEach(x => x.Id = Guid.NewGuid());
            var entities = Mapper.Map<List<Domain.Entities.Business.File>>(fileNames);
            entities.ForEach(x =>
            {
                x.CreatedDate = DateTime.UtcNow;
                x.Url = CryptoHelper.EncryptString(DateTime.UtcNow.ToString(@"dd-MM-yyyy") + @"\" + x.Url);
                x.StorageType = storageType.ToString();
                x.IsPublic = isPublic;
            });
            entities.ForEach(SetEntityCreatedBaseProperties);
            UnitOfWork.Repository.AddRange(entities);
            await UnitOfWork.SaveChangesAsync();
            var result = Mapper.Map<List<FileDto>>(entities);
            return new ResponseResult(result, HttpStatusCode.Created, null, "AddSuccess");
        }

        /// <summary>
        /// Upload Bytes
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> UploadBytes(UploadRequestDto model, int length)
        {
            var basePath = _configuration["StoragePaths:Base"];
            _path = basePath + _configuration["StoragePaths:" + model.AppCode];
            var file = (FileDto)await _storage(model.StorageType.ToString()).StoreBytes(model, _path , model.AppCode);
            file.Id = Guid.NewGuid();
            file.Name = file.Name + "." + model.AttachmentExtension;
            var fileEntity = Mapper.Map<Domain.Entities.Business.File>(file);
            fileEntity.CreatedDate = DateTime.UtcNow;
            fileEntity.DocumentType = model.AttachmentExtension;
            fileEntity.Url = CryptoHelper.EncryptString(DateTime.UtcNow.ToString(@"dd-MM-yyyy") + @"\" + fileEntity.Url);
            fileEntity.StorageType = model.StorageType.ToString();
            fileEntity.IsPublic = model.IsPublic;
            SetEntityCreatedBaseProperties(fileEntity);
            UnitOfWork.Repository.Add(fileEntity);
            await UnitOfWork.SaveChangesAsync();
            var response = new UploadResponseDto
            {
                AttachmentExtension = fileEntity.DocumentType,
                AttachmentName = fileEntity.Name,
                AttachmentType = fileEntity.ContentType,
                AttachmentSize = fileEntity.FileSize,
                FileId = fileEntity.Id
            };
            return new ResponseResult(response, HttpStatusCode.Created, null, "AddSuccess");
        }

        
        /// <summary>
        /// Download With App Code From Token
        /// </summary>
        /// <returns></returns>
        public async Task<object> DownloadWithAppCode(Guid id, string token)
        {
            var file = await UnitOfWork.Repository.GetAsync(id);
            if (file == null) return null;
            if ((string.IsNullOrEmpty(token) || !TokenHelper.CheckToken(token, id)) && !file.IsPublic)
                throw new UnauthorizedAccessException("UnAuthorized To Access This File");
            var fileUrl = CryptoHelper.DecryptString(file.Url);
            var basePath = _configuration["StoragePaths:Base"];
            var path = basePath + _configuration["StoragePaths:" + file.AppCode];
            var memory = (MemoryStream)await _storage(file.StorageType).DownLoad(fileUrl, path);
            var downloadFile = Mapper.Map<DownLoadDto>(file);
            downloadFile.MemoryStream = memory;
            return downloadFile;
        }
      
        /// <summary>
        /// Get Directories
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetDirectoriesAsync(StorageType storageType)
        {
            var result = await _storage(storageType.ToString()).GetDirectoriesAsync();
            return result;
        }

        /// <summary>
        /// Delete File Physical From Database And Folder
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> DeletePhysicalAsync(Guid id)
        {
            var file = await UnitOfWork.Repository.GetAsync(id);
            var decryptedUrl = CryptoHelper.DecryptString(file.Url);
            var basePath = _configuration["StoragePaths:Base"];
            var path = basePath + _configuration["StoragePaths:" + file.AppCode] + decryptedUrl;
            await _storage(file.StorageType).Delete(path);
            await DeleteAsync(file.Id);
            return ResponseResult.PostResult(true, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

    }
}
