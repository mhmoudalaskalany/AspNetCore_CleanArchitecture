using System.Threading.Tasks;
using BackendCore.Api.Controllers.Base;
using BackendCore.Common.Core;
using BackendCore.Service.Services.Lookups.Status;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Api.Controllers.Lookup
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
