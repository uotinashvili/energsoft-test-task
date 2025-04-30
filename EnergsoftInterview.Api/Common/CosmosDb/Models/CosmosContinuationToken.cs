namespace EnergsoftInterview.Api.Common.CosmosDb.Models
{
    using System.Text.Json.Serialization;

    public class CosmosContinuationToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        
        [JsonPropertyName("range")]
        public CosmosTokenRange Range { get; set; } = new();
    }

    public class CosmosTokenRange
    {
        [JsonPropertyName("min")]
        public string Min { get; set; } = string.Empty;
        
        [JsonPropertyName("max")]
        public string Max { get; set; } = string.Empty;
    }
} 