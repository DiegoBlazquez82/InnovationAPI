namespace API.Innovation.Infrastructure.EntityConfigurations
{
    using API.Innovation.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="CatalogCarsEntityTypeConfiguration" />.
    /// </summary>
    internal class CatalogCarsEntityTypeConfiguration
        : IEntityTypeConfiguration<Cars>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{Cars}"/>.</param>
        public void Configure(EntityTypeBuilder<Cars> builder)
        {
            builder.ToTable("Cars");
            builder.HasKey(ci => new { ci.CarId });
            builder.HasIndex(ci => new { ci.Matricula, ci.DNI });
        }
    }
}
