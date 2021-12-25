using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Common.File;
using BackendCore.Common.Helpers.FileHelpers.Crypto;
using BackendCore.Common.Helpers.FileHelpers.StorageHelper;
using BackendCore.Common.Helpers.FileHelpers.Token;
using BackendCore.Entities.Enum;
using BackendCore.Service.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BackendCore.Service.Services.File
{
    public class FileService : BaseService<Entities.Entities.Common.File, AddFileDto, FileDto, Guid, Guid?>, IFileService
    {
        #region Private Fields
        
        private readonly Func<string, IStorageService> _storage;
        private readonly IConfiguration _configuration;
        private string _path;

        #endregion

        #region Constructors

        public FileService(IServiceBaseParameter<Entities.Entities.Common.File> parameters, IConfiguration configuration, Func<string, IStorageService> storage) : base(parameters)
        {
            _configuration = configuration;
            _storage = storage;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Upload To Shared Storage
        /// </summary>
        /// <param name="files"></param>
        /// <param name="storageType"></param>
        /// <param name="isPublic"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public async Task<IResult> UploadToSanStorage(IFormFileCollection files, StorageType storageType, bool isPublic, string appCode)
        {
          
            var basePath = _configuration["StoragePaths:Base"];
            _path = basePath + _configuration["StoragePaths:" + appCode];
            var fileNames = await _storage(storageType.ToString()).StoreToSharedFolder(files, _path, appCode);
            fileNames.ForEach(x => x.Id = Guid.NewGuid());
            var entities = Mapper.Map<List<Entities.Entities.Common.File>>(fileNames);
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
        /// <param name="model"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public async Task<IResult> UploadBytes(UploadRequestDto model, int length)
        {
            var basePath = _configuration["StoragePaths:Base"];
            _path = basePath + _configuration["StoragePaths:" + model.AppCode];
            var file = (FileDto)await _storage(model.StorageType.ToString()).StoreBytes(model, _path , model.AppCode);
            file.Id = Guid.NewGuid();
            file.Name = file.Name + "." + model.AttachmentExtension;
            var fileEntity = Mapper.Map<Entities.Entities.Common.File>(file);
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
        /// <param name="id"></param>
        /// <param name="token"></param>
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
        /// <param name="storageType"></param>
        /// <returns></returns>
        public async Task<object> GetDirectoriesAsync(StorageType storageType)
        {
            var result = await _storage(storageType.ToString()).GetDirectoriesAsync();
            return result;
        }

        /// <summary>
        /// Delete File Physical From Database And Folder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> DeletePhysicalAsync(Guid id)
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

        #endregion

        #region Private Methods
        #endregion
    }
}
