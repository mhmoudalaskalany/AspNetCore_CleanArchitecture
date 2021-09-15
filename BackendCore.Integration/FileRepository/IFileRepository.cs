using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TokenDto = BackendCore.Integration.FileRepository.Dtos.TokenDto;

namespace BackendCore.Integration.FileRepository
{
    public interface IFileRepository
    {
        Task<List<TokenDto>> GetTokens(List<Guid> ids);
    }
}
