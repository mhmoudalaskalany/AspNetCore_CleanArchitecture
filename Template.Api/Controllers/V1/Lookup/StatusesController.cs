using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.Lookups.Status;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Status;
using Template.Common.DTO.Lookup.Status.Parameters;
using Template.Common.Extensions;

namespace Template.Api.Controllers.V1.Lookup
{
    /// <summary>
    /// Statuses Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StatusesController : BaseController
    {
        private readonly IStatusService _statusService;
        /// <summary>
        /// Constructor
        /// </summary>
        public StatusesController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// Get By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(ApiResponse<StatusDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<StatusDto>), 404)]
        public async Task<ActionResult<ApiResponse<StatusDto>>> GetAsync(int id)
        {
            var result = await _statusService.GetByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get By id for edit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        [ProducesResponseType(typeof(ApiResponse<EditStatusDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EditStatusDto>), 404)]
        public async Task<ActionResult<ApiResponse<EditStatusDto>>> GetEditAsync(int id)
        {
            var result = await _statusService.GetEditByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All Statuses
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<StatusDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<StatusDto>>), 400)]
        public async Task<ActionResult<ApiResponse<IEnumerable<StatusDto>>>> GetAllAsync()
        {
            var result = await _statusService.GetAllAsync();
            return result.ToActionResult();
        }

        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<StatusDto>>), 200)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<StatusDto>>), 400)]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<StatusDto>>>> GetPagedAsync([FromBody] BaseParam<StatusFilter> filter)
        {
            var result = await _statusService.GetAllPagedAsync(filter);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All Data paged For Drop Down
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDropDown")]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<StatusDto>>), 200)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<StatusDto>>), 400)]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<StatusDto>>>> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            var result = await _statusService.GetDropDownAsync(filter);
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
        public async Task<ActionResult<ApiResponse<int?>>> AddAsync([FromBody] AddStatusDto dto)
        {
            var result = await _statusService.AddAsync(dto);
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
        public async Task<ActionResult<ApiResponse<int?>>> UpdateAsync(AddStatusDto model)
        {
            var result = await _statusService.UpdateAsync(model);
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
            var result = await _statusService.DeleteAsync(id);
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
            var result = await _statusService.DeleteSoftAsync(id);
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
            var result = await _statusService.DeleteRangeAsync(ids);
            return result.ToActionResult();
        }
    }
}
