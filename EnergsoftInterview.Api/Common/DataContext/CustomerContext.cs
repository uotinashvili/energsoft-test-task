using EnergsoftInterview.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Common.DataContext
{
    public class CustomerContext : ICustomerContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerService _customerService;
        private int? _cachedCustomerId;

        public CustomerContext(IHttpContextAccessor httpContextAccessor, ICustomerService customerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _customerService = customerService;
        }

        public async Task<int> GetCustomerIdAsync()
        {
            if (_cachedCustomerId.HasValue)
                return _cachedCustomerId.Value;

            var token = _httpContextAccessor.HttpContext?.Items["CustomerToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Missing customer token.");

            var customerId = await _customerService.GetCustomerIdAsync(token);

            _cachedCustomerId = customerId;
            return _cachedCustomerId.Value;
        }
    }
} 