namespace API.Innovation.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Users" />.
    /// </summary>
    public class Users
    {
        [Key]
        /// <summary>
        /// Gets or sets the IdUser.
        /// </summary>
        public int IdUser { get; set; }

        [Required]
        /// <summary>
        /// Gets or sets the DNI.
        /// </summary>
        public string DNI { get; set; }

        [Required]
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        [Required]
        /// <summary>
        /// Gets or sets the SubName.
        /// </summary>
        public string SubName { get; set; }

        [Required]
        /// <summary>
        /// Gets or sets the Points.
        /// </summary>
        public int Points { get; set; } = 50;
    }
}
