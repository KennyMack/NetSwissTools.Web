using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Exceptions;
using NetSwissTools.Utils;
using System.Net;

namespace NetSwissTools.Web.Mvc.Results
{
    public static class SwissResultHelpers
    {
        static string IdHeaderName = "Id";

        internal static void AddEntityId(HttpResponse response, string entityId)
        {
            if (response.StatusCode == (int)HttpStatusCode.NoContent ||
                response.StatusCode == (int)HttpStatusCode.MovedPermanently)
            {
                response.Headers.Add(IdHeaderName, entityId);
            }
        }

        internal static Uri GenerateLocationHeader(HttpRequest request, string resourceUrl, string id)
        {
            if (!resourceUrl.IsEmpty())
                return new Uri(resourceUrl);

            var controller = request.RouteValues.FirstOrDefault(x => x.Key.Equals("controller", StringComparison.OrdinalIgnoreCase)).Value ??
                (request.Path.Value ?? "/")[1..];

            var url = $"{request.Scheme}://{request.Host}/{controller}";

            if (!id.IsEmpty())
                url += $"/{id}";

            return new Uri(url);
        }

        internal static IActionResult GenerateActionResult<T>(T entity)
        {
            if (entity == null)
                return new StatusCodeResult((int)HttpStatusCode.NoContent);

            return new ObjectResult(new SwissResult(
                (int)HttpStatusCode.Created,
                entity,
                new List<ModelException>()))
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public static string GetBaseUrl(this HttpContext context)
        {
            var request = context.Request;

            var controller = request.RouteValues.FirstOrDefault(x => x.Key.Equals("controller", StringComparison.OrdinalIgnoreCase)).Value ??
                (request.Path.Value ?? "/")[1..];

            return $"{request.Scheme}://{request.Host}/{controller}";
        }
    }
}
