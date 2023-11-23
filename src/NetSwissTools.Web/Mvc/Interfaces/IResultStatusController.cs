using System.Net;

namespace NetSwissTools.Web.Mvc.Interfaces
{
    public interface IResultStatusController
    {
        bool IsInformational(HttpStatusCode code);
        bool IsSuccess(HttpStatusCode code);
        bool IsRedirect(HttpStatusCode code);
        bool IsClientError(HttpStatusCode code);
        bool IsServerError(HttpStatusCode code);
        bool IsSuccessReponse(HttpStatusCode code);

    }
}
