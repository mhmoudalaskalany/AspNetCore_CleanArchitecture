using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base.V1;
using Template.Application.Services.Lookups.Action;
using Template.Common.Core;

namespace Template.Api.Controllers.Lookup.V1
{
    /// <summary>
    /// Actions Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [HttpGet("getActions")]
        public async Task<IFinalResult> GetActionsAsync() => await _service.GetActionsAsync();


    }
}
