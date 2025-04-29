namespace EnergsoftInterview.Api.Common
{
    public interface ITenantContext
    {
        Task<int> GetTenantIdAsync();
    }
}