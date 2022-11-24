using System.Threading.Tasks;
using Application.Services.Lookups.Action;
using Common.Core;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base;

namespace Template.Api.Controllers.Lookup
{
    /// <summary>
    /// Actions Controller
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
