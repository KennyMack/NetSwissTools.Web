using NetSwissTools.Exceptions;
using NetSwissTools.Web.Mvc.Results;
using System.Net;

namespace NetSwissTools.Web.Mvc.Helpers
{
    internal static class ControllerResults
    {
        internal static SwissCreatedResult<T> Created<T>(string id, T entity) =>
            new(id, entity);

        internal static SwissCreatedResult<T> Created<T>(string resourceUrl, string id, T entity) =>
            new(resourceUrl, id, entity);

        internal static SwissUpdatedResult<T> Updated<T>(string id, T entity) =>
            new(id, entity);

        internal static SwissUpdatedResult<T> Updated<T>(string resourceUrl, string id, T entity) =>
            new(resourceUrl, id, entity);

        internal static SwissOkResult RequestOK(object result) =>
            new(result);

        internal static SwissOkResult RequestOK(object result, HttpStatusCode httpStatusCode) =>
            new(result, httpStatusCode);

        internal static SwissBadRequestResult BadRequest(object data, ModelException[] errors) =>
            new(data, errors);

        internal static SwissBadRequestResult BadRequest(object data, ModelException error) =>
            new(data, error);

        internal static SwissBadRequestResult BadRequest(ModelException error) =>
            new(error);

        internal static SwissBadRequestResult BadRequest(ModelException[] errors) =>
            new(errors);

        internal static SwissBadRequestResult UnprocessableEntity(object data, ModelException[] errors) =>
            new(data, errors);

        internal static SwissBadRequestResult UnprocessableEntity(object data, ModelException error) =>
            new(data, error);

        internal static SwissBadRequestResult UnprocessableEntity(ModelException error) =>
            new(error);

        internal static SwissBadRequestResult UnprocessableEntity(ModelException[] errors) =>
            new(errors);

        internal static SwissNotFoundResult NotFound(ModelException error) =>
            new(error);

        internal static SwissNotFoundResult NotFound() =>
            new();

        internal static SwissNotFoundResult Unauthorized(ModelException error) =>
            new(error);

        internal static SwissNotFoundResult Unauthorized() =>
            new();

        internal static SwissMovedPermanently MovedPermanently(string id) =>
            new(id);

        internal static SwissMovedPermanently MovedPermanently(string id, ModelException error) =>
            new(id, error);

        internal static SwissMovedPermanently MovedPermanently(string resourceUrl, string id) =>
            new(resourceUrl, id);

        internal static SwissMovedPermanently MovedPermanently(string resourceUrl, string id, ModelException error) =>
            new(resourceUrl, id, error);
    }
}
