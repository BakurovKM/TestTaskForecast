using Microsoft.EntityFrameworkCore;

namespace Forecast.Dal.Context
{
    public class ForecastContextMigrations
    {
        private readonly string _connectionString;
        
        public ForecastContextMigrations(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void ApplyMigrations()
        {
            using (var context = new ForecastContext(_connectionString))
            { 
                context.Database.Migrate();
            }
        }
    }
}