using NetSwissTools.Exceptions;
using NetSwissTools.Web.Mvc.Helpers;
using System.Net;

namespace NetSwissTools.Web.Mvc.Results
{
    public class SwissResult
    {
        public SwissResult(
            int status,
            List<ModelException> error)
        {
            this.status = status;
            this.success = ResultStatusHelper.IsSuccessReponse((HttpStatusCode)status);
            this.data = null;
            this.error = error;
        }

        public SwissResult(
            int status, object data,
            List<ModelException> error)
        {
            this.status = status;
            this.success = ResultStatusHelper.IsSuccessReponse((HttpStatusCode)status);
            this.data = data;
            this.error = error;
        }

        public int status { get; private set; }
        public bool success { get; private set; }
        public object data { get; private set; }
        public List<ModelException> error { get; private set; }

    }
}
