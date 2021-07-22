namespace API.Innovation.Infrastructure.EntityConfigurations
{
    using API.Innovation.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="CatalogUsersEntityTypeConfiguration" />.
    /// </summary>
    internal class CatalogUsersEntityTypeConfiguration
        : IEntityTypeConfiguration<Users>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{Users}"/>.</param>
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(o => new { o.IdUser });
            builder.HasIndex(i => new { i.DNI }).IsUnique(); 
        }
    }
}
