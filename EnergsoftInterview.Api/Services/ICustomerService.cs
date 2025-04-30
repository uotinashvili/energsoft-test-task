namespace EnergsoftInterview.Api.Services
{
    public interface ICustomerService
    {
        Task<int> GetCustomerIdAsync(string token);
    }
} 