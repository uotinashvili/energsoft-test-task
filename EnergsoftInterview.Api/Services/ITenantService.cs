namespace EnergsoftInterview.Api.Services
{
    public interface ITenantService
    {
        Task<int> GetTenantIdAsync(string token);
    }
}