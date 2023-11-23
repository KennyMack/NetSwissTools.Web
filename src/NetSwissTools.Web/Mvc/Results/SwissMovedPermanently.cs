using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Exceptions;
using NetSwissTools.Web.Enums;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissMovedPermanently : ObjectResult
    {
        private readonly int ResponseCode = StatusCodes.Status301MovedPermanently;
        private readonly ModelException[] ErrorList;
        public virtual string Id { get; }
        public virtual string ResourceUrl { get; }

        public SwissMovedPermanently(string id) :
            base (null)
        {
            Id = id;
            ResourceUrl = "";
            ErrorList = new List<ModelException>()
            {
                new ModelException
                {
                    ErrorCode = (int)EExceptionErrorCodes.RegisterNotFound,
                    Field = "",
                    Value = "",
                    Messages = new [] { "Moved Permanently" }
                }
            }.ToArray();
        }

        public SwissMovedPermanently(string id, ModelException errors) :
            base(null)
        {
            Id = id;
            ResourceUrl = "";
            ErrorList = new[] { errors };
        }

        public SwissMovedPermanently(string resourceUrl, string id) :
            base(null)
        {
            Id = id;
            ResourceUrl = resourceUrl;
            ErrorList = new List<ModelException>()
            {
                new ModelException
                {
                    ErrorCode = (int)EExceptionErrorCodes.RegisterNotFound,
                    Field = "",
                    Value = "",
                    Messages = new [] { "Moved Permanently" }
                }
            }.ToArray();
        }

        public SwissMovedPermanently(string resourceUrl, string id, ModelException errors) :
            base(null)
        {
            Id = id;
            ResourceUrl = resourceUrl;
            ErrorList = new[] { errors };
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

            HttpRequest request = context.HttpContext.Request;
            HttpResponse response = context.HttpContext.Response;
            Uri location = SwissResultHelpers.GenerateLocationHeader(request, ResourceUrl, Id);

            var ss = location.AbsoluteUri;

            response.Headers["Location"] = location.AbsoluteUri;
            await objResult.ExecuteResultAsync(context).ConfigureAwait(false);
            // SwissResultHelpers.AddEntityId(response, Id);
        }
    }
}
