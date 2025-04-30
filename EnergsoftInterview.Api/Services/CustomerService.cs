using EnergsoftInterview.Api.Common.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _appDbContext;

        public CustomerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> GetCustomerIdAsync(string token)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.Token == token);
            if (customer == null)
                throw new UnauthorizedAccessException("Invalid customer token.");

            return customer.Id;
        }
    }
} 