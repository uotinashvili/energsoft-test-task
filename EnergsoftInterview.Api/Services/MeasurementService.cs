using EnergsoftInterview.Api.Common.DataContext;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Repositories;

namespace EnergsoftInterview.Api.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepositoryFactory _factory;
        private readonly ITenantContext _tenantContext;

        public MeasurementService(
            IMeasurementRepositoryFactory factory,
            ITenantContext tenantContext)
        {
            _factory = factory;
            _tenantContext = tenantContext;
        }

        public async Task<PagedResultDto<MeasurementDto>> GetMeasurementsAsync(int page, int pageSize, string? continuationToken = null)
        {
            var tenantId = await _tenantContext.GetTenantIdAsync();
            var repo = await _factory.CreateAsync(tenantId);

            var result = await repo.GetMeasurementsAsync(tenantId, page, pageSize, continuationToken);

            var measurements = result.Items.Select(m => new MeasurementDto
            {
                Id = m.Id,
                Voltage = m.Voltage,
                Current = m.Current,
                Temperature = m.Temperature,
                Timestamp = m.Timestamp
            });

            return new PagedResultDto<MeasurementDto>
            {
                TotalCount = result.TotalCount,
                Items = measurements,
                ContinuationToken = result.ContinuationToken
            };
        }
    }
}