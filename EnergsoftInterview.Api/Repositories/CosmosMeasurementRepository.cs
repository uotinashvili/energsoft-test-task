using EnergsoftInterview.Api.Common.CosmosDb.Configuration;
using EnergsoftInterview.Api.Common.CosmosDb.Helpers;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace EnergsoftInterview.Api.Repositories
{
    public class CosmosMeasurementRepository : IMeasurementRepository
    {
        private readonly Container _container;

        public CosmosMeasurementRepository(IOptions<CosmosDbSettings> options)
        {
            var settings = options.Value;
            var client = new CosmosClient(settings.ConnectionString);
            _container = client.GetDatabase(settings.DatabaseName).GetContainer(settings.ContainerName);
        }

        public async Task<PagedResultDto<Measurement>> GetMeasurementsAsync(int customerId, int page, int pageSize, string? continuationToken = null)
        {
            var processedToken = ContinuationTokenHelper.DecodeToken(continuationToken);
            var results = await GetMeasurementsPageAsync(customerId, pageSize, processedToken);
            var totalCount = await GetTotalCountAsync(customerId);
            var formattedToken = ContinuationTokenHelper.EncodeToken(results.ContinuationToken);

            return new PagedResultDto<Measurement>
            {
                TotalCount = totalCount,
                Items = results.Items,
                ContinuationToken = formattedToken
            };
        }

        private async Task<(List<Measurement> Items, string? ContinuationToken)> GetMeasurementsPageAsync(int customerId, int pageSize, string? continuationToken)
        {
            var queryDefinition = new QueryDefinition(
                "SELECT * FROM c WHERE c.CustomerId = @customerId ORDER BY c.Timestamp DESC")
                .WithParameter("@customerId", customerId);

            var queryRequestOptions = new QueryRequestOptions
            {
                MaxItemCount = pageSize
            };

            var results = new List<Measurement>();
            using var resultSetIterator = _container.GetItemQueryIterator<Measurement>(
                queryDefinition,
                continuationToken,
                queryRequestOptions);

            var response = await resultSetIterator.ReadNextAsync();
            results.AddRange(response);

            return (results, response.ContinuationToken);
        }

        private async Task<int> GetTotalCountAsync(int customerId)
        {
            var countQuery = new QueryDefinition("SELECT VALUE COUNT(1) FROM c WHERE c.CustomerId = @customerId")
                .WithParameter("@customerId", customerId);

            var countOptions = new QueryRequestOptions { PartitionKey = new PartitionKey(customerId) };
            using var countIterator = _container.GetItemQueryIterator<int>(countQuery, requestOptions: countOptions);

            int totalCount = 0;
            while (countIterator.HasMoreResults)
            {
                var countResponse = await countIterator.ReadNextAsync();
                totalCount += countResponse.FirstOrDefault();
            }

            return totalCount;
        }
    }
}