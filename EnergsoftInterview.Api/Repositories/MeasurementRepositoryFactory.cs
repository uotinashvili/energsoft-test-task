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

        public async Task<IMeasurementRepository> CreateAsync(int tenantId)
        {
            var tenant = await _appDbContext.Tenants.FindAsync(tenantId);
            return tenant?.DataSource switch
            {
                DataSourceType.CosmosDb => _serviceProvider.GetRequiredService<CosmosMeasurementRepository>(),
                _ => _serviceProvider.GetRequiredService<SqlMeasurementRepository>()
            };
        }
    }
}