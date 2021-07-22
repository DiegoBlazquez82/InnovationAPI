namespace API.Innovation.Infrastructure
{
    using API.Innovation.Infrastructure.EntityConfigurations;
    using API.Innovation.Infrastructure.Models;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Defines the <see cref="DbAPIContext" />.
    /// </summary>
    public class DbAPIContext : DbContext
    {
        /// <summary>
        /// Gets or sets the Cars.
        /// </summary>
        public DbSet<Cars> Cars { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// Gets or sets the Infraction.
        /// </summary>
        public DbSet<Infraction> Infraction { get; set; }

        /// <summary>
        /// Gets or sets the VehicleViolation.
        /// </summary>
        public DbSet<VehicleViolation> VehicleViolation { get; set; }

        /// <summary>
        /// Defines the _configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbAPIContext"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public DbAPIContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// The OnConfiguring.
        /// </summary>
        /// <param name="optionsBuilder">The optionsBuilder<see cref="DbContextOptionsBuilder"/>.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = _configuration.GetValue<string>("ConnectionString") };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        /// <summary>
        /// The OnModelCreating.
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogCarsEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogUsersEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogInfractionEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogVehicleViolationEntityTypeConfiguration());
        }
    }
}
