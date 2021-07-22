namespace API.Innovation.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Infraction" />.
    /// </summary>
    public class Infraction
    {
        [Key]
        /// <summary>
        /// Gets or sets the IdInfringement.
        /// </summary>
        public int IdInfraction { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the PointUnless.
        /// </summary>
        public int PointUnless { get; set; }
    }
}
