using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base;
using Template.Application.Services.Lookups.Category;
using Template.Common.Core;

namespace Template.Api.Controllers.Lookup
{
    /// <summary>
    /// Actions Controller
    /// </summary>
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IFinalResult> GetCategoriesAsync() => await _service.GetCategoriesAsync();


    }
}
