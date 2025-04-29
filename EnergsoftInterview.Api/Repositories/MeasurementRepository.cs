using EnergsoftInterview.Api.Data;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Repositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly AppDbContext _appDbContext;

        public MeasurementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PagedResult<Measurement>> GetMeasurementsAsync(int tenantId, int page, int pageSize)
        {
            var query = _appDbContext.Measurements
                .Where(m => m.TenantId == tenantId)
                .OrderByDescending(m => m.Timestamp);

            var totalCount = await query.CountAsync();
            var measurements = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return new PagedResult<Measurement>
            {
                TotalCount = totalCount,
                Items = measurements
            };
        }
    }
}