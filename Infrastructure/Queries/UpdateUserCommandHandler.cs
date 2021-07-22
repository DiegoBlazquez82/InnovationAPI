using API.Innovation.Infrastructure;
using API.Innovation.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Innovation.Application.Command
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly DbAPIContext _context;

        public UpdateUserCommandHandler(DbAPIContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = this._context.Users.Where(x => x.DNI == request.Users.DNI).FirstOrDefault();

            user.DNI = request.Users.DNI ?? user.DNI;
            user.Name = request.Users.Name ?? user.Name;
            user.SubName  = request.Users.SubName ?? user.SubName;
            
            if (request.Users.Points != 0)
                user.Points = request.Users.Points;

            this._context.Entry(user).State = EntityState.Modified;

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
