using EnergsoftInterview.Api.Common;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Repositories;

namespace EnergsoftInterview.Api.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly ITenantContext _tenantContext;

        public MeasurementService(
            IMeasurementRepository measurementRepository,
            ITenantContext tenantContext)
        {
            _measurementRepository = measurementRepository;
            _tenantContext = tenantContext;
        }

        public async Task<PagedResult<MeasurementDto>> GetMeasurementsAsync(int page, int pageSize)
        {
            var tenantId = await _tenantContext.GetTenantIdAsync();

            var result = await _measurementRepository.GetMeasurementsAsync(tenantId, page, pageSize);

            var measurements = result.Items.Select(m => new MeasurementDto
            {
                Id = m.Id,
                Voltage = m.Voltage,
                Current = m.Current,
                Temperature = m.Temperature,
                Timestamp = m.Timestamp
            });

            return new PagedResult<MeasurementDto>
            {
                TotalCount = result.TotalCount,
                Items = measurements
            };
        }
    }
}