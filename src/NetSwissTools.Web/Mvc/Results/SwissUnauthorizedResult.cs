using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Exceptions;
using NetSwissTools.Web.Enums;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissUnauthorizedResult : UnauthorizedResult
    {
        private readonly int ResponseCode = StatusCodes.Status404NotFound;
        private readonly ModelException[] ErrorList;

        public SwissUnauthorizedResult()
        {
            ErrorList = new List<ModelException>()
            {
                new ModelException
                {
                    ErrorCode = (int)EExceptionErrorCodes.RegisterNotFound,
                    Field = "",
                    Value = "",
                    Messages = new [] { "Not Authorized" }
                }
            }.ToArray();
        }

        public SwissUnauthorizedResult(ModelException errors)
        {
            ErrorList = new[] { errors };
        }

        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = ResponseCode;

            ObjectResult objResult = new(new SwissResult(
                ResponseCode,
                ErrorList.ToList()))
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
                ErrorList.ToList()))
            {
                StatusCode = ResponseCode
            };
            await objResult.ExecuteResultAsync(context).ConfigureAwait(false);
        }
    }
}
