using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Entities;

namespace EnergsoftInterview.Api.Repositories
{
    public interface IMeasurementRepository
    {
        Task<PagedResultDto<Measurement>> GetMeasurementsAsync(int customerId, int page, int pageSize, string? continuationToken = null);
    }
}