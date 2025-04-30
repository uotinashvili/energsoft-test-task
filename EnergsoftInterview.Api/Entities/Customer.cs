using EnergsoftInterview.Api.Common.Enums;

namespace EnergsoftInterview.Api.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ApiKey { get; set; } = default!;
        public DataSourceType DataSource { get; set; }
    }
} 