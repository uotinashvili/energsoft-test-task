using Moq;
using EnergsoftInterview.Api.Services;
using EnergsoftInterview.Api.Entities;
using EnergsoftInterview.Api.Repositories;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Common;

namespace EnergsoftInterview.Tests.Services
{
    public class MeasurementServiceTests
    {
        [Fact]
        public async Task GetMeasurementsAsync_ReturnsData()
        {
            var tenantId = 1;
            var page = 1;
            var pageSize = 10;

            var mockTenantContext = new Mock<ITenantContext>();
            mockTenantContext.Setup(tc => tc.GetTenantIdAsync())
                             .ReturnsAsync(tenantId);

            var measurements = new List<Measurement>
            {
                new Measurement { Id = 1, Voltage = 230, Current = 10, Temperature = 25, Timestamp = DateTime.UtcNow },
                new Measurement { Id = 2, Voltage = 220, Current = 11, Temperature = 26, Timestamp = DateTime.UtcNow }
            };

            var mockRepo = new Mock<IMeasurementRepository>();
            mockRepo.Setup(repo => repo.GetMeasurementsAsync(tenantId, page, pageSize))
                    .ReturnsAsync(new PagedResult<Measurement>
                    {
                        TotalCount = measurements.Count,
                        Items = measurements
                    });

            var service = new MeasurementService(mockRepo.Object, mockTenantContext.Object);

            var result = await service.GetMeasurementsAsync(page, pageSize);

            Assert.NotNull(result);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(230, result.Items.First().Voltage);
        }
    }
}