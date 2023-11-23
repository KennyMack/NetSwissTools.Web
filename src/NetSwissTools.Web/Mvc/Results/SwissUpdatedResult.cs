using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissUpdatedResult<T> : ObjectResult
    {
        public virtual T Entity { get; }
        public virtual string Id { get; }
        public virtual string ResourceUrl { get; }

        public SwissUpdatedResult(string id, T entity)
           : base(entity)
        {
            Entity = entity;
            Id = id;
            ResourceUrl = "";
        }

        public SwissUpdatedResult(string resourceUrl, string id, T entity)
          : base(entity)
        {
            Entity = entity;
            Id = id;
            ResourceUrl = resourceUrl;
        }

        public async override Task ExecuteResultAsync(ActionContext context)
        {
            IActionResult result = GenerateActionResult();
            await result.ExecuteResultAsync(context).ConfigureAwait(false);
        }

        internal IActionResult GenerateActionResult()
        {
            if (Entity == null)
                return new StatusCodeResult((int)HttpStatusCode.NoContent);

            return new ObjectResult(Entity)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
