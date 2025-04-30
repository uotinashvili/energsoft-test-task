using EnergsoftInterview.Api.DTOs;

namespace EnergsoftInterview.Api.Services
{
    public interface IMeasurementService
    {
        Task<PagedResultDto<MeasurementDto>> GetMeasurementsAsync(int page, int pageSize, string? continuationToken = null);
    }
}