using System.Threading.Tasks;
using BackendCore.Api.Controllers.Base;
using BackendCore.Common.Core;
using BackendCore.Service.Services.Lookups.Action;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Api.Controllers.Lookup
{
    /// <summary>
    /// Lookups Controller
    /// </summary>
    public class ActionsController : BaseController
    {
        private readonly IActionService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public ActionsController(IActionService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get All Actions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IFinalResult> GetActionsAsync() => await _service.GetActionsAsync();


    }
}
