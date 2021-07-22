namespace API.Innovation.Infrastructure.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Index(nameof(Matricula), nameof(DNI))]
    /// <summary>
    /// Defines the <see cref="Cars" />.
    /// </summary>
    public class Cars
    {
        /// <summary>
        /// Gets or sets the CarId.
        /// </summary>
        [Key]
        /// <summary>
        /// Gets or sets the CarId.
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Gets or sets the Matricula.
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Gets or sets the Marca.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Gets or sets the Modelo.
        /// </summary>
        public string modelo { get; set; }

        /// <summary>
        /// Gets or sets the DNI.
        /// </summary>
        [ForeignKey("DNI")]
        public string DNI { get; set; }
    }
}
