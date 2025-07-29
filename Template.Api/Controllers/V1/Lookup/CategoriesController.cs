using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.Lookups.Category;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Category;
using Template.Common.DTO.Lookup.Category.Parameters;
using Template.Common.Extensions;

namespace Template.Api.Controllers.V1.Lookup
{
    /// <summary>
    /// Categories Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), 404)]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> GetAsync(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get By id for edit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getEdit/{id}")]
        [ProducesResponseType(typeof(ApiResponse<EditCategoryDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EditCategoryDto>), 404)]
        public async Task<ActionResult<ApiResponse<EditCategoryDto>>> GetEditAsync(int id)
        {
            var result = await _categoryService.GetEditByIdAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDto>>), 400)]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAllAsync()
        {
            var result = await _categoryService.GetAllAsync();
            return result.ToActionResult();
        }

        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost("getPaged")]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<CategoryDto>>), 200)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<CategoryDto>>), 400)]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<CategoryDto>>>> GetPagedAsync([FromBody] BaseParam<CategoryFilter> filter)
        {
            var result = await _categoryService.GetAllPagedAsync(filter);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All Data paged For Drop Down
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDropDown")]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<CategoryDto>>), 200)]
        [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<CategoryDto>>), 400)]
        public async Task<ActionResult<ApiPagedResponse<IEnumerable<CategoryDto>>>> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            var result = await _categoryService.GetDropDownAsync(filter);
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
        public async Task<ActionResult<ApiResponse<int?>>> AddAsync([FromBody] AddCategoryDto dto)
        {
            var result = await _categoryService.AddAsync(dto);
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
        public async Task<ActionResult<ApiResponse<int?>>> UpdateAsync(AddCategoryDto model)
        {
            var result = await _categoryService.UpdateAsync(model);
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
            var result = await _categoryService.DeleteAsync(id);
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
            var result = await _categoryService.DeleteSoftAsync(id);
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
            var result = await _categoryService.DeleteRangeAsync(ids);
            return result.ToActionResult();
        }
    }
}
