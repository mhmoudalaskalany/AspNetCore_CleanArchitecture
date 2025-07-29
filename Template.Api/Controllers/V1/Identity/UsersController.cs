using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.Identity.User;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.User;
using Template.Common.DTO.Identity.User.Parameters;
using Template.Common.Extensions;

namespace Template.Api.Controllers.V1.Identity
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public UsersController(IUserService userService)
        {
            _service = userService;
        }
        /// <summary>
        /// Get By id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetAsync(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get By id for edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        public async Task<ActionResult<ApiResponse<EditUserDto>>> GetEditAsync(Guid id)
        {
            var result = await _service.GetEditByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return result.ToActionResult();
        }

        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<UserDto>>>> GetPagedAsync([FromBody] BaseParam<UserFilter> filter)
        {
            var result = await _service.GetAllPagedAsync(filter);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All Data paged For Drop Down
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDropDown")]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<UserDto>>>> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            var result = await _service.GetDropDownAsync(filter);
            return result.ToActionResult();
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<Guid?>>> AddAsync([FromBody] AddUserDto dto)
        {
            var result = await _service.AddAsync(dto);
            return result.ToActionResult();
        }

        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<Guid?>>> UpdateAsync(AddUserDto model)
        {
            var result = await _service.UpdateAsync(model);
            return result.ToActionResult();
        }

        /// <summary>
        /// Remove by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Soft Remove by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("deleteSoft/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteSoftAsync(Guid id)
        {
            var result = await _service.DeleteSoftAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Bulk Remove by ids
        /// </summary>
        /// <param name="ids">PK</param>
        /// <returns></returns>
        [HttpDelete("deleteRange")]
        public async Task<ActionResult<ApiResponse>> DeleteRangeAsync(List<Guid> ids)
        {
            var result = await _service.DeleteRangeAsync(ids);
            return result.ToActionResult();
        }
    }
}
