using System.Net;

namespace NetSwissTools.Web.Mvc.Helpers
{
    public static class ResultStatusHelper
    {
        public static bool IsClientError(HttpStatusCode code) =>
            (int)code >= 400 && (int)code <= 499;

        public static bool IsInformational(HttpStatusCode code) =>
            (int)code >= 100 && (int)code <= 199;

        public static bool IsRedirect(HttpStatusCode code) =>
            (int)code >= 300 && (int)code <= 399;

        public static bool IsServerError(HttpStatusCode code) =>
            (int)code >= 500 && (int)code <= 599;

        public static bool IsSuccess(HttpStatusCode code) =>
            (int)code >= 200 && (int)code <= 299;

        public static bool IsSuccessReponse(HttpStatusCode code)
        {
            if (IsInformational(code) || IsSuccess(code) || IsRedirect(code))
                return true;
            else if (IsClientError(code) || IsServerError(code))
                return false;

            return false;
        }
    }
}
