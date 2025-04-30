namespace EnergsoftInterview.Api.Common.CosmosDb.Configuration
{
    public class CosmosDbSettings
    {
        public string? ConnectionString { get; set; } = string.Empty;
        public string? DatabaseName { get; set; } = string.Empty;
        public string? ContainerName { get; set; } = string.Empty;
        public string? AzureTenantId { get; set; } = string.Empty; // For Azure AD authentication
    }
} 