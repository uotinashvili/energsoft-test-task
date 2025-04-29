namespace EnergsoftInterview.Api.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}