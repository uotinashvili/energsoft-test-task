namespace EnergsoftInterview.Api.Entities
{
    public class Measurement
    {
        public int Id { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
    }
}