namespace EnergsoftInterview.Api.Common.DataContext
{
    public interface ICustomerContext
    {
        Task<int> GetCustomerIdAsync();
    }
} 