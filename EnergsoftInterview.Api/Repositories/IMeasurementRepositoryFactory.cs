namespace EnergsoftInterview.Api.Repositories
{
    public interface IMeasurementRepositoryFactory
    {
        Task<IMeasurementRepository> CreateAsync(int customerId);
    }
} 