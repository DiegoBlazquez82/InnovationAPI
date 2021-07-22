using API.Innovation.Infrastructure;
using API.Innovation.Infrastructure.Models;
using API.Innovation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, bool>
    {
        private readonly DbAPIContext _context;

        public CreateCarCommandHandler(DbAPIContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var user = this._context.Users.Where(u => u.DNI == request.Car.DNI).ToList();
            if (user == null || user.Count == 0)
            {
                throw new IndexOutOfRangeException("No existe el conductor");
            }

            var cars = this._context.Cars.Where(x => x.DNI == request.Car.DNI).ToList();
            if (cars == null || cars.Count < 10)
            {
                _context.Cars.Add(request.Car);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new IndexOutOfRangeException("Maximo numero de vehiculos para el conductor"); 
            }
        }
    }
}
