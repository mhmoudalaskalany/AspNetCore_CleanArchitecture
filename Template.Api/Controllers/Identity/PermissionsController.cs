using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base;
using Template.Application.Services.Identity.Permission;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.Permission;
using Template.Common.DTO.Identity.Permission.Parameters;

namespace Template.Api.Controllers.Identity
{
    /// <summary>
    /// Permissions Controller
    /// </summary>
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
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IFinalResult> GetAsync(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id For Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IFinalResult> GetEditAsync(long id)
        {
            var result = await _service.GetEditByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataPaging> GetPagedAsync([FromBody] BaseParam<PermissionFilter> filter)
        {
            return await _service.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IFinalResult> AddAsync([FromBody] AddPermissionDto dto)
        {
            var result = await _service.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IFinalResult> UpdateAsync(AddPermissionDto model)
        {
            return await _service.UpdateAsync(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IFinalResult> DeleteAsync(long id)
        {
            return await _service.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(long id)
        {
            return await _service.DeleteSoftAsync(id);
        }


    }
}
