using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.Common.Core;

namespace Template.Common.Extensions
{
    /// <summary>
    /// Extension methods to convert Result to ActionResult
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts a Result to an ActionResult
        /// </summary>
        public static ActionResult<ApiResponse> ToActionResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(ApiResponse.SuccessResponse(result.Message));
            }

            return new BadRequestObjectResult(ApiResponse.ErrorResponse(result.Message, HttpStatusCode.BadRequest, result.Errors));
        }

        /// <summary>
        /// Converts a Result<T> to an ActionResult<ApiResponse<T>>
        /// </summary>
        public static ActionResult<ApiResponse<T>> ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(ApiResponse<T>.SuccessResponse(result.Data, result.Message));
            }

            return new BadRequestObjectResult(ApiResponse<T>.ErrorResponse(result.Message, HttpStatusCode.BadRequest, result.Errors));
        }

        /// <summary>
        /// Converts a Result<T> to an ActionResult<ApiResponse<T>> with custom status codes
        /// </summary>
        public static ActionResult<ApiResponse<T>> ToActionResult<T>(this Result<T> result, HttpStatusCode successStatusCode)
        {
            if (result.IsSuccess)
            {
                var response = ApiResponse<T>.SuccessResponse(result.Data, result.Message, successStatusCode);
                return successStatusCode switch
                {
                    HttpStatusCode.Created => new CreatedResult(string.Empty, response),
                    HttpStatusCode.NoContent => new NoContentResult(),
                    HttpStatusCode.Accepted => new AcceptedResult(string.Empty, response),
                    _ => new OkObjectResult(response)
                };
            }

            return new BadRequestObjectResult(ApiResponse<T>.ErrorResponse(result.Message, HttpStatusCode.BadRequest, result.Errors));
        }

        /// <summary>
        /// Converts a PagedResult<T> to an ActionResult<ApiPagedResponse<T>>
        /// </summary>
        public static ActionResult<ApiPagedResponse<T>> ToActionResult<T>(this PagedResult<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(ApiPagedResponse<T>.SuccessResponse(
                    result.Data, result.PageNumber, result.PageSize, result.TotalCount, result.Message));
            }

            return new BadRequestObjectResult(ApiPagedResponse<T>.ErrorResponse(
                result.Message, result.PageNumber, result.PageSize, HttpStatusCode.BadRequest, result.Errors));
        }

        /// <summary>
        /// Converts a PagedResult<T> to an ActionResult<ApiPagedResponse<T>> with custom status codes
        /// </summary>
        public static ActionResult<ApiPagedResponse<T>> ToActionResult<T>(this PagedResult<T> result, HttpStatusCode successStatusCode)
        {
            if (result.IsSuccess)
            {
                var response = ApiPagedResponse<T>.SuccessResponse(
                    result.Data, result.PageNumber, result.PageSize, result.TotalCount, result.Message, successStatusCode);
                
                return successStatusCode switch
                {
                    HttpStatusCode.Created => new CreatedResult(string.Empty, response),
                    HttpStatusCode.NoContent => new NoContentResult(),
                    HttpStatusCode.Accepted => new AcceptedResult(string.Empty, response),
                    _ => new OkObjectResult(response)
                };
            }

            return new BadRequestObjectResult(ApiPagedResponse<T>.ErrorResponse(
                result.Message, result.PageNumber, result.PageSize, HttpStatusCode.BadRequest, result.Errors));
        }
    }
}
