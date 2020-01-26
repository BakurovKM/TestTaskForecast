using Forecast.Dal.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace Forecast.Dal.Context
{
    internal class ForecastContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Models.Forecast> Forecasts { get; set; }
        public DbSet<CityForecast> CityForecasts { get; set; }
        public DbSet<DayForecast> DayForecasts { get; set; }
        public DbSet<City> Cities { get; set; }
        
        public ForecastContext()
        {
        }

        public ForecastContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<City>(city => city.Property(c => c.Name).HasMaxLength(128));
            builder.Entity<DayForecast>(df => df.Property(c => c.Describe).HasMaxLength(128));
            builder.Entity<DayForecast>(df => df.Property(c => c.Date).HasColumnType("Date"));
            builder.Entity<DayForecast>(df => df.Property(c => c.Precipitation).HasColumnType("decimal(5,2)"));
            builder.Entity<DayForecast>(df => df.Property(c => c.WindSpeedMs).HasColumnType("decimal(5,2)"));
        }
    }
}