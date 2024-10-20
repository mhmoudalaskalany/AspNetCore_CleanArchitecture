using System.Threading.Tasks;
using System.Web.Services.Description;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.Lookups.Action;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Action;
using Template.Common.DTO.Lookup.Action.Parameters;

namespace Template.Api.Controllers.V1.Lookup
{
    /// <summary>
    /// Actions Controller
    /// </summary>
    [ApiVersion("1.0")]
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
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<IFinalResult> GetAsync(int id) => await _actionService.GetByIdAsync(id);


        /// <summary>
        /// Get By Id For Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        public async Task<IFinalResult> GetEditAsync(int id) => await _actionService.GetEditByIdAsync(id);


        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IFinalResult> GetAllAsync() => await _actionService.GetAllAsync();


        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        public async Task<DataPaging> GetPagedAsync([FromBody] BaseParam<ActionFilter> filter) => await _actionService.GetAllPagedAsync(filter);


        /// <summary>
        /// Get All Data paged For Drop Down
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDropDown")]
        public async Task<DataPaging> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter) => await _actionService.GetDropDownAsync(filter);
        


        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IFinalResult> AddAsync([FromBody] AddActionDto dto) => await _actionService.AddAsync(dto);


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IFinalResult> UpdateAsync(AddActionDto model) => await _actionService.UpdateAsync(model);

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IFinalResult> DeleteAsync(int id) => await _actionService.DeleteAsync(id);


        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("deleteSoft/{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(int id) => await _actionService.DeleteSoftAsync(id);
    }
}
