using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TokenDto = BackendCore.Common.DTO.Common.File.TokenDto;

namespace BackendCore.Integration.FileRepository
{
    public interface IFileRepository
    {
        /// <summary>
        /// Get Tokens Using File Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TokenDto>> GetTokens(List<Guid> ids);
    }
}
