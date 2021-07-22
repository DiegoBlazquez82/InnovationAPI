using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Innovation.DGT.Infrastructure.Models
{
    public class AddInfractionCarModel
    {
        /// <summary>
        /// Gets or sets the DNI.
        /// </summary>
        public string DNI { get; set; }

        /// <summary>
        /// Gets or sets the IdInfringement.
        /// </summary>
        public int IdInfraction { get; set; }

        /// <summary>
        /// Gets or sets the Matricula.
        /// </summary>
        public string Matricula { get; set; }

    }
}
