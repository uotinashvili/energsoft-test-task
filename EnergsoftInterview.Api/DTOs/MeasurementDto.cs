namespace EnergsoftInterview.Api.DTOs
{
    public class MeasurementDto
    {
        public int Id { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}