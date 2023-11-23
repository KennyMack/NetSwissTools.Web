namespace NetSwissTools.Web.Mvc.Interfaces
{
    public interface IUrlInfoController
    {
        IQueryCollection QueryString { get; }
        int GetPageNumber();
        int GetPageSize();
        string GetSort();
        string GetFilter();
        string GetQueryColumn();
        DateTime? GetDateTime(string pDate);
    }
}
