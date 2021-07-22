namespace API.Innovation.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="VehicleViolation" />.
    /// </summary>
    public class VehicleViolation
    {
        [Key]
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        [ForeignKey("User")]
        /// <summary>
        /// Gets or sets the IdUser.
        /// </summary>
        public int IdUser { get; set; }

        [ForeignKey("Vehicle")]
        /// <summary>
        /// Gets or sets the IdVehicle.
        /// </summary>
        public int IdVehicle { get; set; }

        [ForeignKey("Infraction")]
        /// <summary>
        /// Gets or sets the IdVehicle.
        /// </summary>
        public int IdInfraction{ get; set; }

        /// <summary>
        /// Gets or sets the RegisterTime.
        /// </summary>
        public DateTime RegisterTime { get; set; }
    }
}