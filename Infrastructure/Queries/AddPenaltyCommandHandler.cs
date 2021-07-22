using API.Innovation.Infrastructure;
using API.Innovation.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Innovation.DGT.Infrastructure.Queries
{
    public class AddPenaltyCommandHandler : IRequestHandler<AddPenaltyCommand, bool>
    {
        private readonly DbAPIContext _context;

        public AddPenaltyCommandHandler(DbAPIContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(AddPenaltyCommand request, CancellationToken cancellationToken)
        {
            var user = this._context.Users.Where(x=> x.DNI == request.Vehicle.DNI).FirstOrDefault();
            if (user == null)
            {
                throw new IndexOutOfRangeException("No existe el conductor");
            }

            var vehicle = this._context.Cars.Where(x => x.Matricula == request.Vehicle.Matricula).FirstOrDefault();
            if (vehicle == null)
            {
                throw new IndexOutOfRangeException("No existe ningun vehiculo con esa matricula");
            }

            var infraction = await this._context.Infraction.FindAsync(request.Vehicle.IdInfraction);
            if (infraction == null)
            {
                throw new IndexOutOfRangeException("No existe la infraccion");
            }
            try
            {
                var car = new VehicleViolation()
                {
                    IdInfraction = request.Vehicle.IdInfraction,
                    IdUser = user.IdUser,
                    IdVehicle = vehicle.CarId
                };

                // Se guarda la infracion del vehiculo y el conductor
                _context.VehicleViolation.Add(car);
                await _context.SaveChangesAsync();
            
                //Se actualizan los puntos 
                user.Points = user.Points - infraction.PointUnless;
                this._context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
    }
}
