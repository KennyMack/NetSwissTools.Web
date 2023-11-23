using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Exceptions;
using NetSwissTools.Utils;
using System.Net;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissCreatedResult<T> : ObjectResult
    {
        public virtual T Entity { get; }
        public virtual string Id { get; }
        public virtual string ResourceUrl { get; }

        public SwissCreatedResult(string id, T entity)
           : base(entity)
        {
            Entity = entity;
            Id = id;
            ResourceUrl = "";
        }

        public SwissCreatedResult(string resourceUrl, string id, T entity)
          : base(entity)
        {
            Entity = entity;
            Id = id;
            ResourceUrl = resourceUrl;
        }

        public async override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpRequest request = context.HttpContext.Request;
            HttpResponse response = context.HttpContext.Response;
            IActionResult result = SwissResultHelpers.GenerateActionResult(Entity);
            Uri location = SwissResultHelpers.GenerateLocationHeader(request, ResourceUrl, Id);

            response.Headers["Location"] = location.AbsoluteUri;
            await result.ExecuteResultAsync(context).ConfigureAwait(false);
            SwissResultHelpers.AddEntityId(response, Id);
        }

        

        
    }
}
