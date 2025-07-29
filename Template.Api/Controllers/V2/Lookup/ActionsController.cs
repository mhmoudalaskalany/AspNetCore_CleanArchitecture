using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.Lookups.Action;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Action;
using Template.Common.DTO.Lookup.Action.Parameters;
using Template.Common.Extensions;

namespace Template.Api.Controllers.V2.Lookup
{
    /// <summary>
    /// Actions Controller
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ActionsController : BaseController
    {
        private readonly IActionService _actionService;
        /// <summary>
        /// Constructor
        /// </summary>
        public ActionsController(IActionService actionService)
        {
            _actionService = actionService;
        }

        /// <summary>
        /// Get By id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(ApiResponse<ActionDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<ActionDto>), 404)]
        public async Task<ActionResult<ApiResponse<ActionDto>>> GetAsync(int id)
        {
            var result = await _actionService.GetByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get By id for edit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        [ProducesResponseType(typeof(ApiResponse<EditActionDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EditActionDto>), 404)]
        public async Task<ActionResult<ApiResponse<EditActionDto>>> GetEditAsync(int id)
        {
            var result = await _actionService.GetEditByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All Actions
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActionDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ActionDto>>), 400)]
        public async Task<ActionResult<ApiResponse<IEnumerable<ActionDto>>>> GetAllAsync()
        {
            var result = await _actionService.GetAllAsync();
            return result.ToActionResult();
        }

        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<ActionDto>>), 200)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<ActionDto>>), 400)]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<ActionDto>>>> GetPagedAsync([FromBody] BaseParam<ActionFilter> filter)
        {
            var result = await _actionService.GetAllPagedAsync(filter);
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
        public async Task<ActionResult<ApiResponse<int?>>> AddAsync([FromBody] AddActionDto dto)
        {
            var result = await _actionService.AddAsync(dto);
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
        public async Task<ActionResult<ApiResponse<int?>>> UpdateAsync(AddActionDto model)
        {
            var result = await _actionService.UpdateAsync(model);
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
        public async Task<ActionResult<ApiResponse>> DeleteAsync(int id)
        {
            var result = await _actionService.DeleteAsync(id);
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
        public async Task<ActionResult<ApiResponse>> DeleteSoftAsync(int id)
        {
            var result = await _actionService.DeleteSoftAsync(id);
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
            var result = await _actionService.DeleteRangeAsync(ids);
            return result.ToActionResult();
        }
    }
}
