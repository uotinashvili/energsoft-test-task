using EnergsoftInterview.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
    }
}