using EnergsoftInterview.Api.Common.DataContext;
using EnergsoftInterview.Api.Common.Enums;

namespace EnergsoftInterview.Api.Repositories
{
    public class MeasurementRepositoryFactory : IMeasurementRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppDbContext _appDbContext;

        public MeasurementRepositoryFactory(IServiceProvider serviceProvider, AppDbContext appDbContext)
        {
            _serviceProvider = serviceProvider;
            _appDbContext = appDbContext;
        }

        public async Task<IMeasurementRepository> CreateAsync(int customerId)
        {
            var customer = await _appDbContext.Customers.FindAsync(customerId);
            return customer?.DataSource switch
            {
                DataSourceType.CosmosDb => _serviceProvider.GetRequiredService<CosmosMeasurementRepository>(),
                _ => _serviceProvider.GetRequiredService<SqlMeasurementRepository>()
            };
        }
    }
}