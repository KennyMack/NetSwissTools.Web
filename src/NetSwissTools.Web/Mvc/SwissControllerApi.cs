using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetSwissTools.Exceptions;
using NetSwissTools.Services.Interfaces;
using NetSwissTools.Utils;
using NetSwissTools.Web.Enums;
using NetSwissTools.Web.Mvc.Helpers;
using NetSwissTools.Web.Mvc.Interfaces;
using NetSwissTools.Web.Mvc.Results;
using System.Net;
using System.Text.RegularExpressions;

namespace NetSwissTools.Web.Mvc
{
    [ApiController]
    public abstract class SwissControllerApi : ControllerBase//, IErrorsController, IUrlInfoController, IResultStatusController
    {
        private readonly List<ModelException> Errors = new();
        protected IErrorBaseService[] Services { get; set; }
        protected IQueryCollection QueryString { get => HttpContext.Request.Query; }

        protected void AddError(Exception exception) =>
            Errors.Add(new ModelException
            {
                ErrorCode = (int)EExceptionErrorCodes.UnhandledException,
                Field = "",
                Messages = new[] { exception.Message },
                Value = ""
            });

        protected void AddError(string exception) =>
            Errors.Add(new ModelException
            {
                ErrorCode = (int)EExceptionErrorCodes.UnhandledException,
                Field = "",
                Messages = new[] { exception },
                Value = ""
            });

        protected void AddError(string field, string exception) =>
            Errors.Add(new ModelException
            {
                ErrorCode = (int)EExceptionErrorCodes.UnhandledException,
                Field = field,
                Messages = new[] { exception },
                Value = ""
            });

        protected void AddError(string field, string exception, string value) =>
           Errors.Add(new ModelException
           {
               ErrorCode = (int)EExceptionErrorCodes.UnhandledException,
               Field = field,
               Messages = new[] { exception },
               Value = value
           });

        protected void AddError(int code, string field, string exception, string value) =>
           Errors.Add(new ModelException
           {
               ErrorCode = code,
               Field = field,
               Messages = new[] { exception },
               Value = value
           });

        protected void AddError(ModelException exception) =>
            Errors.Add(exception);

        protected void AddError(ModelException[] exceptions) =>
            Errors.AddRange(exceptions);

        protected void ClearErrors() =>
            Errors.Clear();

        protected ModelException[] GetErrors()
        {
            if (IsOperationValid())
                return Errors.ToArray();

            if (Services != null)
            {
                return Services
                    .SelectMany(r => r.Errors)
                    .Select(r => r)
                    .ToArray();
            }

            return null;
        }

        protected bool HasAnyErrors()
        {
            if (!IsOperationValid())
                return true;

            if (Services != null)
                return Services.Where(r => r.Errors.Any()).Any();

            return false;
        }

        protected bool IsOperationValid() =>
            !Errors.Any();

        protected bool IsClientError(HttpStatusCode code) =>
            ResultStatusHelper.IsClientError(code);

        protected bool IsInformational(HttpStatusCode code) =>
            ResultStatusHelper.IsInformational(code);

        protected bool IsRedirect(HttpStatusCode code) =>
            ResultStatusHelper.IsRedirect(code);

        protected bool IsServerError(HttpStatusCode code) =>
            ResultStatusHelper.IsServerError(code);

        protected bool IsSuccess(HttpStatusCode code) =>
            ResultStatusHelper.IsSuccess(code);

        protected bool IsSuccessReponse(HttpStatusCode code) =>
            ResultStatusHelper.IsSuccessReponse(code);

        protected int GetPageNumber()
        {
            int page;
            try
            {
                var value = QueryString["page"];
                if (!int.TryParse(value, out page))
                    page = 1;
            }
            catch (Exception)
            {
                page = 1;
            }

            return page;
        }

        protected int GetPageSize()
        {
            int limit;
            try
            {
                var value = QueryString["limit"];
                if (!int.TryParse(value, out limit))
                    limit = 10;
            }
            catch (Exception)
            {
                limit = 10;
            }

            return limit;
        }

        protected string GetSort()
        {
            string sort;

            try
            {
                sort = QueryString["sort"].ToString().ToUpper();
            }
            catch (Exception)
            {
                sort = "";
            }
            return sort;
        }

        protected string GetFilter()
        {
            string filter;

            try
            {
                filter = QueryString["filter"].ToString().ToUpper();
            }
            catch (Exception)
            {
                filter = "";
            }
            return filter;
        }

        protected string GetQueryColumn()
        {
            string column;

            try
            {
                column = QueryString["column"];
            }
            catch (Exception)
            {
                column = "";
            }
            return column;
        }

        protected DateTime? GetDateTime(string pDate)
        {
            try
            {
                string[] date = null;
                string[] time = new string[]
                {
                    "00",
                    "00"
                };
                string seconds = "00";

                if (pDate.Length >= 8)
                {
                    date = new[]
                    {
                        pDate.SubStr(0, 2),
                        pDate.SubStr(2, 2),
                        pDate.SubStr(4, 4)
                    };
                }

                if (date == null)
                    return null;

                if (pDate.Length >= 12)
                {
                    time = new[]
                    {
                        pDate.SubStr(8, 2),
                        pDate.SubStr(10, 2)
                    };
                }

                if (pDate.Length >= 14)
                    seconds = pDate.SubStr(12, 2);


                return new DateTime(
                    Convert.ToInt32(date[2]),
                    Convert.ToInt32(date[1]),
                    Convert.ToInt32(date[0]),
                    Convert.ToInt32(time[0]),
                    Convert.ToInt32(time[1]),
                    Convert.ToInt32(seconds));
            }
            catch (Exception)
            {

            }
            return null;
        }

        protected bool ValidateModelState<TEntity>(TEntity pModel) where TEntity : class
        {
            ModelState.Clear();
            var result = TryValidateModel(pModel);

            foreach (var item in ModelState.ToList())
            {
                if (item.Value.Errors.Any())
                {
                    var modelError = new ModelException
                    {
                        ErrorCode = (int)EExceptionErrorCodes.ValidationError,
                        Field = item.Key,
                        Messages = item.Value.Errors.Select(x => x.ErrorMessage).ToArray(),
                        Value = ""
                    };

                    Errors.Add(modelError);
                }
            }

            return result;
        }

        protected virtual SwissCreatedResult<T> Created<T>(string id, T entity) =>
            ControllerResults.Created(id, entity);

        protected virtual SwissCreatedResult<T> Created<T>(string resourceUrl, string id, T entity) =>
            ControllerResults.Created(resourceUrl, id, entity);

        protected virtual SwissUpdatedResult<T> Updated<T>(string id, T entity) =>
            ControllerResults.Updated(id, entity);

        protected virtual SwissUpdatedResult<T> Updated<T>(string resourceUrl, string id, T entity) =>
            ControllerResults.Updated(resourceUrl, id, entity);

        protected virtual SwissOkResult RequestOK(object result) =>
            ControllerResults.RequestOK(result);

        protected virtual SwissOkResult RequestOK(object result, HttpStatusCode httpStatusCode) =>
            ControllerResults.RequestOK(result, httpStatusCode);

        protected virtual SwissBadRequestResult BadRequest(object data, ModelException[] errors) =>
            ControllerResults.BadRequest(data, errors);

        protected virtual SwissBadRequestResult BadRequest(object data, ModelException error) =>
            ControllerResults.BadRequest(data, error);

        protected virtual SwissBadRequestResult BadRequest(ModelException error) =>
            ControllerResults.BadRequest(error);

        protected virtual SwissBadRequestResult BadRequest(ModelException[] errors) =>
            ControllerResults.BadRequest(errors);

        protected virtual SwissBadRequestResult UnprocessableEntity(object data, ModelException[] errors) =>
            ControllerResults.UnprocessableEntity(data, errors);

        protected virtual SwissBadRequestResult UnprocessableEntity(object data, ModelException error) =>
            ControllerResults.UnprocessableEntity(data, error);

        protected virtual SwissBadRequestResult UnprocessableEntity(ModelException error) =>
            ControllerResults.UnprocessableEntity(error);

        protected virtual SwissBadRequestResult UnprocessableEntity(ModelException[] errors) =>
            ControllerResults.UnprocessableEntity(errors);

        protected virtual SwissNotFoundResult NotFound(ModelException error) =>
            ControllerResults.NotFound(error);

        protected new virtual SwissNotFoundResult NotFound() =>
            ControllerResults.NotFound();

        protected virtual SwissNotFoundResult Unauthorized(ModelException error) =>
            ControllerResults.Unauthorized(error);

        protected new virtual SwissNotFoundResult Unauthorized() =>
            ControllerResults.Unauthorized();

        protected virtual SwissMovedPermanently MovedPermanently(string id) =>
            ControllerResults.MovedPermanently(id);

        protected virtual SwissMovedPermanently MovedPermanently(string id, ModelException error) =>
            ControllerResults.MovedPermanently(id, error);

        protected virtual SwissMovedPermanently MovedPermanently(string resourceUrl, string id) =>
            ControllerResults.MovedPermanently(resourceUrl, id);

        protected virtual SwissMovedPermanently MovedPermanently(string resourceUrl, string id, ModelException error) =>
            ControllerResults.MovedPermanently(resourceUrl, id, error);

    }
}
