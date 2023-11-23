using NetSwissTools.Exceptions;

namespace NetSwissTools.Web.Mvc.Interfaces
{
    public interface IErrorService
    {
        List<ModelException> Errors { get; set; }
    }
}
