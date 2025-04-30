using Moq;
using EnergsoftInterview.Api.Services;
using EnergsoftInterview.Api.Entities;
using EnergsoftInterview.Api.Repositories;
using EnergsoftInterview.Api.DTOs;
using EnergsoftInterview.Api.Common.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Tests.Services
{
    public class TestAppDbContext : AppDbContext
    {
        public TestAppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }

    public class MeasurementServiceTests
    {
        [Fact]
        public async Task GetMeasurementsAsync_ReturnsData()
        {
            var customerId = 1;
            var page = 1;
            var pageSize = 10;

            var mockCustomerContext = new Mock<ICustomerContext>();
            mockCustomerContext.Setup(tc => tc.GetCustomerIdAsync())
                           .ReturnsAsync(customerId);

            var measurements = new List<Measurement>
            {
                new Measurement { Id = 1, Voltage = 230, Current = 10, Temperature = 25, Timestamp = DateTime.UtcNow },
                new Measurement { Id = 2, Voltage = 220, Current = 11, Temperature = 26, Timestamp = DateTime.UtcNow }
            };

            var mockRepo = new Mock<IMeasurementRepository>();
            mockRepo.Setup(repo => repo.GetMeasurementsAsync(
                It.IsAny<int>(), 
                It.IsAny<int>(), 
                It.IsAny<int>(), 
                It.IsAny<string?>()))
                   .ReturnsAsync(new PagedResultDto<Measurement>
                   {
                       TotalCount = measurements.Count,
                       Items = measurements,
                       ContinuationToken = null
                   });

            var mockFactory = new Mock<IMeasurementRepositoryFactory>();
            mockFactory.Setup(f => f.CreateAsync(customerId))
                      .ReturnsAsync(mockRepo.Object);

            var service = new MeasurementService(mockFactory.Object, mockCustomerContext.Object);

            var result = await service.GetMeasurementsAsync(page, pageSize);

            Assert.NotNull(result);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(230, result.Items.First().Voltage);
        }
    }
}