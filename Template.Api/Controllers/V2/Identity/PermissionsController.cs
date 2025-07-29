using System.Collections.Generic;
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

namespace Template.Api.Controllers.V2.Identity
{
    /// <summary>
    /// Permissions Controller
    /// </summary>
    [ApiVersion("2.0")]
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
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ApiResponse<PermissionDto>>> GetAsync(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get By id for edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
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
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<PermissionDto>>>> GetPagedAsync([FromBody] BaseParam<PermissionFilter> filter)
        {
            var result = await _service.GetAllPagedAsync(filter);
            return result.ToActionResult();
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
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
        public async Task<ActionResult<ApiResponse>> DeleteRangeAsync(List<int> ids)
        {
            var result = await _service.DeleteRangeAsync(ids);
            return result.ToActionResult();
        }
    }
}
