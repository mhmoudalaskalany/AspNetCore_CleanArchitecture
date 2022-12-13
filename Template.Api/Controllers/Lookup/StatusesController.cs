using System.Threading.Tasks;
using Common.Core;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base;
using Template.Application.Services.Lookups.Status;

namespace Template.Api.Controllers.Lookup
{
    /// <summary>
    /// Statuses Controller
    /// </summary>
    public class StatusesController : BaseController
    {
        private readonly IStatusService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public StatusesController(IStatusService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Statuses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IFinalResult> GetStatusesAsync() => await _service.GetStatusesAsync();




    }
}
