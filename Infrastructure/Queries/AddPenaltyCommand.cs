using API.Innovation.DGT.Infrastructure.Models;
using API.Innovation.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Innovation.DGT.Infrastructure.Queries
{
    public class AddPenaltyCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the Matricula.
        /// </summary>
        public AddInfractionCarModel Vehicle { get; set; }

        public AddPenaltyCommand() { }

        public AddPenaltyCommand(AddInfractionCarModel vehicle) : this()
        {
            Vehicle = vehicle;
        }
    }
}
