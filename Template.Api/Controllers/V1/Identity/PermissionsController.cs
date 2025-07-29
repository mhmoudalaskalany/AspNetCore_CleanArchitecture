using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.Identity.Permission;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.Permission;
using Template.Common.DTO.Identity.Permission.Parameters;
using Template.Common.Extensions;

namespace Template.Api.Controllers.V1.Identity
{
    /// <summary>
    /// Permissions Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PermissionsController : BaseController
    {
        private readonly IPermissionService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public PermissionsController(IPermissionService permissionService)
        {
            _service = permissionService;
        }
        /// <summary>
        /// Get By id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(ApiResponse<PermissionDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<PermissionDto>), 404)]
        public async Task<ActionResult<ApiResponse<PermissionDto>>> GetAsync(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get By id for edit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        [ProducesResponseType(typeof(ApiResponse<EditPermissionDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EditPermissionDto>), 404)]
        public async Task<ActionResult<ApiResponse<EditPermissionDto>>> GetEditAsync(long id)
        {
            var result = await _service.GetEditByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<PermissionDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<PermissionDto>>), 400)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PermissionDto>>>> GetAllAsync()
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
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<PermissionDto>>), 200)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<PermissionDto>>), 400)]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<PermissionDto>>>> GetPagedAsync([FromBody] BaseParam<PermissionFilter> filter)
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
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<PermissionDto>>), 200)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<PermissionDto>>), 400)]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<PermissionDto>>>> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
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
        [ProducesResponseType(typeof(ApiResponse<int?>), 200)]
        [ProducesResponseType(typeof(ApiResponse<int?>), 400)]
        public async Task<ActionResult<ApiResponse<int?>>> AddAsync([FromBody] AddPermissionDto dto)
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
        [ProducesResponseType(typeof(ApiResponse<int?>), 200)]
        [ProducesResponseType(typeof(ApiResponse<int?>), 400)]
        public async Task<ActionResult<ApiResponse<int?>>> UpdateAsync(AddPermissionDto model)
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
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<ApiResponse>> DeleteAsync(long id)
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
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<ApiResponse>> DeleteSoftAsync(long id)
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
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult<ApiResponse>> DeleteRangeAsync(List<int> ids)
        {
            var result = await _service.DeleteRangeAsync(ids);
            return result.ToActionResult();
        }
    }
}
