using API.Innovation.Infrastructure;
using API.Innovation.Infrastructure.Models;
using API.Innovation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Innovation.Application.Command
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, bool>
    {
        private readonly DbAPIContext _context;

        public UpdateCarCommandHandler(DbAPIContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = this._context.Cars.Where(x => x.Matricula == request.Car.Matricula).FirstOrDefault();

            car.DNI = request.Car.DNI ?? car.DNI;
            car.Marca = request.Car.Marca ?? car.Marca;
            car.modelo = request.Car.modelo ?? car.modelo;
            car.DNI = request.Car.DNI ?? car.DNI;

            this._context.Entry(car).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }
    }
}
