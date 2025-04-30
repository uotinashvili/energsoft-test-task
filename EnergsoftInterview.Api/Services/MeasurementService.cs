using EnergsoftInterview.Api.Common.DataContext;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Repositories;

namespace EnergsoftInterview.Api.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepositoryFactory _factory;
        private readonly ICustomerContext _customerContext;

        public MeasurementService(
            IMeasurementRepositoryFactory factory,
            ICustomerContext customerContext)
        {
            _factory = factory;
            _customerContext = customerContext;
        }

        public async Task<PagedResultDto<MeasurementDto>> GetMeasurementsAsync(int page, int pageSize, string? continuationToken = null)
        {
            var customerId = await _customerContext.GetCustomerIdAsync();
            var repo = await _factory.CreateAsync(customerId);

            var result = await repo.GetMeasurementsAsync(customerId, page, pageSize, continuationToken);

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