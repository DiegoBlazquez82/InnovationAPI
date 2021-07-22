namespace API.Innovation.Infrastructure.EntityConfigurations
{
    using API.Innovation.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="CatalogInfractionEntityTypeConfiguration" />.
    /// </summary>
    internal class CatalogInfractionEntityTypeConfiguration
        : IEntityTypeConfiguration<Infraction>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{Infraction}"/>.</param>
        public void Configure(EntityTypeBuilder<Infraction> builder)
        {
            builder.ToTable("Infraction");
            builder.HasKey(ci => ci.IdInfraction);
        }
    }
}
