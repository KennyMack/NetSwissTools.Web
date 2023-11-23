using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Exceptions;
using System.Net;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissOkResult: OkResult
    {
        private readonly int ResponseCode = StatusCodes.Status200OK;
        private readonly object Data;

        public SwissOkResult(object data)
        {
            Data = data;
        }

        public SwissOkResult(HttpStatusCode statusCode, object data)
        {
            ResponseCode = (int)statusCode;
            Data = data;
        }

        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = ResponseCode;

            ObjectResult objResult = new(new SwissResult(
                ResponseCode,
                Data,
                new List<ModelException>())
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
                new List<ModelException>())
            )
            {
                StatusCode = ResponseCode
            };
            await objResult.ExecuteResultAsync(context).ConfigureAwait(false);
        }
    }
}
