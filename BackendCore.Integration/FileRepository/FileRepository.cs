﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.Helpers.HttpClient;
using BackendCore.Common.Helpers.HttpClient.RestSharp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using TokenDto = BackendCore.Integration.FileRepository.Dtos.TokenDto;

namespace BackendCore.Integration.FileRepository
{
    public class FileRepository : IFileRepository
    {
        private readonly IRestSharpContainer _restSharpContainer;
        private readonly MicroServicesUrls _urls;
        private readonly IConfiguration _configuration;
        public FileRepository(IRestSharpContainer restSharpContainer, IConfiguration configuration, MicroServicesUrls urls)
        {
            _restSharpContainer = restSharpContainer;
            _configuration = configuration;
            _urls = urls;
        }



        public async Task<List<TokenDto>> GetTokens(List<Guid> ids)
        {
            var appCode = _configuration["AppCode"];
            var result = await _restSharpContainer.SendRequest<ResponseResult>(_urls.GenerateTokenWithClaims +"/" +appCode, Method.POST, ids);
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(JsonConvert.SerializeObject(result.Data));
            return tokens;
        }



    }
}
