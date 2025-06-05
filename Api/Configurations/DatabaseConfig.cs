using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations
{
    public static class DatabaseConfig
    {
        public static WebApplicationBuilder AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddDbContext<DbContextProject>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }
    }
}
