using EnergsoftInterview.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Services
{
    public class TenantService : ITenantService
    {
        private readonly AppDbContext _appDbContext;

        public TenantService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> GetTenantIdAsync(string token)
        {
            var tenant = await _appDbContext.Tenants.FirstOrDefaultAsync(t => t.Token == token);
            if (tenant == null)
                throw new UnauthorizedAccessException("Invalid tenant token.");

            return tenant.Id;
        }
    }
}