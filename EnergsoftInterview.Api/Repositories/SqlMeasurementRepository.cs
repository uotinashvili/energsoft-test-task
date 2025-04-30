using EnergsoftInterview.Api.Common.DataContext;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Repositories
{
    public class SqlMeasurementRepository : IMeasurementRepository
    {
        private readonly AppDbContext _appDbContext;

        public SqlMeasurementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PagedResultDto<Measurement>> GetMeasurementsAsync(int tenantId, int page, int pageSize, string? continuationToken = null)
        {
            var query = _appDbContext.Measurements
                .Where(m => m.TenantId == tenantId)
                .OrderByDescending(m => m.Timestamp);

            var totalCount = await query.CountAsync();
            var measurements = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<Measurement>
            {
                TotalCount = totalCount,
                Items = measurements
            };
        }
    }
}