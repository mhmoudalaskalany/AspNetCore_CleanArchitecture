﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Template.Common.Core;
using Template.Common.Helpers.HttpClient;
using Template.Common.Helpers.HttpClient.RestSharp;
using TokenDto = Template.Common.DTO.Common.File.TokenDto;

namespace Template.Integration.FileRepository
{
    public class FileRepository : IFileRepository
    {
        #region Properties
        private readonly IRestSharpContainer _restSharpContainer;
        private readonly MicroServicesUrls _urls;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        public FileRepository(IRestSharpContainer restSharpContainer, IConfiguration configuration, MicroServicesUrls urls)
        {
            _restSharpContainer = restSharpContainer;
            _configuration = configuration;
            _urls = urls;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get File Tokens Using File Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<TokenDto>> GetTokens(List<Guid> ids)
        {
            var appCode = _configuration["AppCode"];
            var result = await _restSharpContainer.SendRequest<ResponseResult>(_urls.GenerateTokenWithClaims + "/" + appCode, Method.Post, ids);
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(JsonConvert.SerializeObject(result.Data));
            return tokens;
        }

        #endregion




    }
}
