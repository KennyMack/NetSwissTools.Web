using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Exceptions;
using System.Net;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissUnprocessableEntityResult: UnprocessableEntityResult
    {
        private readonly ModelException[] ErrorList;
        private readonly int ResponseCode = StatusCodes.Status422UnprocessableEntity;
        private readonly object Data;

        public SwissUnprocessableEntityResult(ModelException[] errors)
        {
            ErrorList = errors;
            Data = null;
        }

        public SwissUnprocessableEntityResult(HttpStatusCode statusCode, ModelException[] errors)
        {
            ResponseCode = (int)statusCode;
            ErrorList = errors;
            Data = null;
        }

        public SwissUnprocessableEntityResult(ModelException errors)
        {
            ErrorList = new[] { errors };
            Data = null;
        }

        public SwissUnprocessableEntityResult(HttpStatusCode statusCode, ModelException errors)
        {
            ResponseCode = (int)statusCode;
            ErrorList = new[] { errors };
            Data = null;
        }

        public SwissUnprocessableEntityResult(object data, ModelException[] errors)
        {
            ErrorList = errors;
            Data = data;
        }

        public SwissUnprocessableEntityResult(HttpStatusCode statusCode, object data, ModelException[] errors)
        {
            ResponseCode = (int)statusCode;
            ErrorList = errors;
            Data = data;
        }

        public SwissUnprocessableEntityResult(object data, ModelException errors)
        {
            ErrorList = new[] { errors };
            Data = data;
        }

        public SwissUnprocessableEntityResult(HttpStatusCode statusCode, object data, ModelException errors)
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
