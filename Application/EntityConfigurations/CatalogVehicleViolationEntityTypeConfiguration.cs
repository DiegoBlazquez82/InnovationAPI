namespace API.Innovation.Infrastructure.EntityConfigurations
{
    using API.Innovation.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="CatalogVehicleViolationEntityTypeConfiguration" />.
    /// </summary>
    internal class CatalogVehicleViolationEntityTypeConfiguration
        : IEntityTypeConfiguration<VehicleViolation>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{VehicleViolation}"/>.</param>
        public void Configure(EntityTypeBuilder<VehicleViolation> builder)
        {
            builder.ToTable("VehicleViolation");

            builder.HasKey(ci => ci.Id);
        }
    }
}
