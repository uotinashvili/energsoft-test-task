namespace EnergsoftInterview.Api.Common.DataContext
{
    public interface ITenantContext
    {
        Task<int> GetTenantIdAsync();
    }
}