using EnergsoftInterview.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Common.DataContext
{
    public class CustomerContext : ICustomerContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtAuthenticationService _jwtService;
        private int? _cachedCustomerId;

        public CustomerContext(IHttpContextAccessor httpContextAccessor, JwtAuthenticationService jwtService)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
        }

        public async Task<int> GetCustomerIdAsync()
        {
            if (_cachedCustomerId.HasValue)
                return await Task.FromResult(_cachedCustomerId.Value);

            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                throw new UnauthorizedAccessException("Missing or invalid authorization header.");

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var customerId = _jwtService.ValidateToken(token);
            
            if (!customerId.HasValue)
                throw new UnauthorizedAccessException("Invalid JWT token.");

            _cachedCustomerId = customerId.Value;
            return await Task.FromResult(_cachedCustomerId.Value);
        }
    }
} 