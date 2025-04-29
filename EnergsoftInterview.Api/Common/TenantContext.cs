using EnergsoftInterview.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Common
{
    public class TenantContext : ITenantContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;
        private int? _cachedTenantId;

        public TenantContext(IHttpContextAccessor httpContextAccessor, ITenantService tenantService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }

        public async Task<int> GetTenantIdAsync()
        {
            if (_cachedTenantId.HasValue)
                return _cachedTenantId.Value;

            var token = _httpContextAccessor.HttpContext?.Items["TenantToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Missing tenant token.");

            var tenantId = await _tenantService.GetTenantIdAsync(token);

            _cachedTenantId = tenantId;
            return _cachedTenantId.Value;
        }
    }
}