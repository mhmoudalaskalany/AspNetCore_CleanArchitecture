using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base.V1;
using Template.Application.Services.Lookups.Status;
using Template.Common.Core;

namespace Template.Api.Controllers.Lookup.V1
{
    /// <summary>
    /// Statuses Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [HttpGet("getStatuses")]
        public async Task<IFinalResult> GetStatusesAsync() => await _service.GetStatusesAsync();
    }
}
