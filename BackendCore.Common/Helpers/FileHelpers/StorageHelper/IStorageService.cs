using System.Collections.Generic;
using System.Threading.Tasks;
using BackendCore.Common.DTO.Common.File;
using Microsoft.AspNetCore.Http;

namespace BackendCore.Common.Helpers.FileHelpers.StorageHelper
{
    public interface IStorageService
    {
        Task<IEnumerable<object>> StoreToSharedFolder(IFormFileCollection files, string path , string appCode);
        Task<object> StoreBytes(UploadRequestDto dto, string path , string appCode);
        Task<object> DownLoad(string url, string path);
        Task<bool> Delete(string path);
        Task<IEnumerable<object>> GetDirectoriesAsync();

    }
}
