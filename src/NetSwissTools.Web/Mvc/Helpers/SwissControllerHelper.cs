using Microsoft.AspNetCore.Mvc;
using NetSwissTools.Models;
using NetSwissTools.Services.Interfaces;
using NetSwissTools.Utils;
using NetSwissTools.Web.Enums;
using NetSwissTools.Web.Mvc.Interfaces;
using System.Linq.Expressions;
using System.Net;

namespace NetSwissTools.Web.Mvc.Helpers
{
    public static class SwissControllerHelper
    {
        public static Type ResourceMessages { get; set; } = typeof(Resources.MessagesResource);

        public static string DefaultListTitleError = Resources.MessagesResource.ListError;
        public static string DefaultSaveTitleError = Resources.MessagesResource.SaveError;
        public static string DefaultExceptionTitleError = Resources.MessagesResource.ServerError;
        public static string DefaultRemoveTitleError = Resources.MessagesResource.RemoveError;

        public static async Task<IActionResult> GetByKeyAsync<T>(this SwissControllerApi controller, CancellationToken cancellationToken,
            IReaderService<T> service, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            service.Errors.Clear();
            try
            {
                var result = await service.GetByKeyAsync(expr, includeProperties, cancellationToken);
                return ResultOperation(controller, result, service);
            }
            catch (Exception ex)
            {
                return ResultErrorOperation(controller, ex);
            }
        }

        public static async Task<IActionResult> GetAllPagedAsync<T>(this SwissControllerApi controller, CancellationToken cancellationToken, 
            IReaderService<T> service, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            service.Errors.Clear();
            try
            {
                var result = await service.GetAllPagedAsync(pageNumber, pageSize, includeProperties, cancellationToken);
                return ResultOperation(controller, result, service);
            }
            catch (Exception ex)
            {
                return ResultErrorOperation(controller, ex);
            }
        }

        public static async Task<IActionResult> GetAllPagedFilteredAsync<T>(this SwissControllerApi controller, CancellationToken cancellationToken, 
            IReaderService<T> service, int pageNumber, int pageSize,
            Expression<Func<T, bool>> expr,
            params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            service.Errors.Clear();
            try
            {
                var result = await service.GetAllPagedFilteredAsync(pageNumber, pageSize, expr, includeProperties, cancellationToken);
                return ResultOperation(controller, result, service);
            }
            catch (Exception ex)
            {
                return ResultErrorOperation(controller, ex);
            }
        }

        public static async Task<IActionResult> RemoveByIdAsync<T>(this SwissControllerApi controller, CancellationToken cancellationToken, 
            IWriterService<T> service,
            Expression<Func<T, bool>> expr) where T : class
        {
            service.Errors.Clear();
            try
            {
                var result = await service.RemoveByKeyAsync(expr, cancellationToken);

                if (result != null && !service.Errors.Any())
                    await service.SaveChangesAsync(cancellationToken);

                return ResultOperation(controller, result, service);
            }
            catch (Exception ex)
            {
                return ResultErrorOperation(controller, ex);
            }
        }

        public static IActionResult ResultErrorOperation(
            this SwissControllerApi controller,
            Exception ex,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            string TitleError = "")
        {
            if (TitleError.IsEmpty())
                TitleError = DefaultExceptionTitleError;

            var type = controller.GetType();
            return ControllerResults.BadRequest(null, 
                new Exceptions.ModelException
                {
                    ErrorCode = (int)EExceptionErrorCodes.ValidationError,
                    Messages = new[] { ex.Message },
                    Value = $"{type.Namespace}.{type.Name}",
                    Field = TitleError
                });
        }

        public static IActionResult ResultOperation<T>(this SwissControllerApi controller,
           PagedResult<T> OkResult,
           IErrorBaseService service) where T : class
        {
            if (service.Errors.Any())
                return ControllerResults.BadRequest(
                    null,
                    service.Errors.ToArray());

            return ControllerResults.RequestOK(OkResult);
        }

        public static IActionResult ResultOperation<T>(this SwissControllerApi controller,
            T OkResult,
            IErrorBaseService service) where T : class
        {
            if (service.Errors.Any())
                return ControllerResults.BadRequest(
                    null,
                    service.Errors.ToArray());

            return ControllerResults.RequestOK(OkResult);
        }

        public static IActionResult ResultOperation<T>(this SwissControllerApi controller,
            IEnumerable<T> OkResult,
            IErrorBaseService service) where T : class
        {
            if (service.Errors.Any())
                return ControllerResults.BadRequest(
                    null,
                    service.Errors.ToArray());

            return ControllerResults.RequestOK(OkResult);
        }


        public static IActionResult CreatedResultOperation<T>(
            this SwissControllerApi controller,
            string id,
            T OkResult,
            IErrorBaseService service,
            string TitleError = "") where T : class
        {
            if (service.Errors.Any())
                return ControllerResults.BadRequest(
                    null,
                    service.Errors.ToArray());

            return ControllerResults.Created(id, OkResult);
        }

        public static IActionResult CreatedResultOperation<T>(this SwissControllerApi controller,
            string id,
            IEnumerable<T> OkResult,
            IErrorBaseService service) where T : class
        {
            if (service.Errors.Any())
                return ControllerResults.BadRequest(
                    null,
                    service.Errors.ToArray());

            return ControllerResults.Created(id, OkResult);
        }
    }
}
