using NetSwissTools.Exceptions;

namespace NetSwissTools.Web.Mvc.Interfaces
{
    public interface IErrorsController
    {
        void AddError(string message);
        void AddError(string field, string exception);
        void AddError(string field, string exception, string value);
        void AddError(int code, string field, string exception, string value);
        void AddError(ModelException exception);
        void AddError(ModelException[] exceptions);
        void AddError(Exception exception);
        void ClearErrors();
        ModelException[] GetErrors();
        bool HasAnyErrors();
        bool IsOperationValid();
        bool ValidateModelState<TEntity>(TEntity pModel) where TEntity : class;
    }
}
