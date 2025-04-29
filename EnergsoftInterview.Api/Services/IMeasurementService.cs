using EnergsoftInterview.Api.DTOs;

namespace EnergsoftInterview.Api.Services
{
    public interface IMeasurementService
    {
        Task<PagedResult<MeasurementDto>> GetMeasurementsAsync(int page, int pageSize);
    }
}