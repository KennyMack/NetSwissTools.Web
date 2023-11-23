using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Exceptions;
using NetSwissTools.Web.Mvc.Helpers;
using System.Diagnostics;
using System.Net;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissBadRequestResult : BadRequestResult
    {
        private readonly ModelException[] ErrorList;
        private readonly int ResponseCode = StatusCodes.Status400BadRequest;
        private readonly object Data;

        public SwissBadRequestResult(ModelException[] errors)
        {
            ErrorList = errors;
            Data = null;
        }

        public SwissBadRequestResult(HttpStatusCode statusCode, ModelException[] errors)
        {
            ResponseCode = (int)statusCode;
            ErrorList = errors;
            Data = null;
        }

        public SwissBadRequestResult(ModelException errors)
        {
            ErrorList = new[] { errors };
            Data = null;
        }

        public SwissBadRequestResult(HttpStatusCode statusCode, ModelException errors)
        {
            ResponseCode = (int)statusCode;
            ErrorList = new[] { errors };
            Data = null;
        }

        public SwissBadRequestResult(object data, ModelException[] errors)
        {
            ErrorList = errors;
            Data = data;
        }

        public SwissBadRequestResult(HttpStatusCode statusCode, object data, ModelException[] errors)
        {
            ResponseCode = (int)statusCode;
            ErrorList = errors;
            Data = data;
        }

        public SwissBadRequestResult(object data, ModelException errors)
        {
            ErrorList = new[] { errors };
            Data = data;
        }

        public SwissBadRequestResult(HttpStatusCode statusCode, object data, ModelException errors)
        {
            ResponseCode = (int)statusCode;
            ErrorList = new[] { errors };
            Data = data;
        }

        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = ResponseCode;

            ObjectResult objResult = new(new SwissResult(
                ResponseCode,
                Data,
                ErrorList != null ? ErrorList.ToList() : new List<ModelException>())
            )
            {
                StatusCode = ResponseCode
            };

            objResult.ExecuteResult(context);
        }

        public async override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = ResponseCode;

            ObjectResult objResult = new(new SwissResult(
                ResponseCode,
                Data,
                ErrorList != null ? ErrorList.ToList() : new List<ModelException>())
            )
            {
                StatusCode = ResponseCode
            };
            await objResult.ExecuteResultAsync(context).ConfigureAwait(false);
        }
    }
}
