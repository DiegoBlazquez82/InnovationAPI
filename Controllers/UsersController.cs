namespace API.Innovation.Controllers
{
    using API.Innovation.Application.Command;
    using API.Innovation.Infrastructure;
    using API.Innovation.Infrastructure.Models;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="UsersController" />.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Defines the _context.
        /// </summary>
        private readonly DbAPIContext _context;

        /// <summary>
        /// Defines the _mediator.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="DbAPIContext"/>.</param>
        /// <param name="mediat">The mediat<see cref="IMediator"/>.</param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public UsersController(DbAPIContext context, IMediator mediat)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._mediator = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        // GET: api/Users
        /// <summary>
        /// The GetUsers.
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{Users}}}"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// The GetUsers.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Users}}"/>.</returns>
        [HttpGet]
        [Route("{DNI}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Users> GetUsers(string dni)
        {
           var users = _context.Users.Where(x => x.DNI == dni).FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        /// <summary>
        /// The PutUsers.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="users">The users<see cref="Users"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPut]
        [Route("{DNI}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutUsers(string dni, Users users)
        {
            if (dni != users.DNI)
            {
                return BadRequest();
            }
            var createCars = new UpdateUserCommand(users);
            var command = await _mediator.Send(createCars);

            if (command)
                return Ok();
            else
                return BadRequest();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// The PostUsers.
        /// </summary>
        /// <param name="users">The users<see cref="Users"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Users}}"/>.</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            try
            {
                _context.Users.Add(users);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUsers", new { id = users.IdUser }, users);
            }
            catch (DbUpdateException inex)
            {
                if (inex.InnerException.Message.Contains ("UNIQUE"))
                    return BadRequest("Ya existe un usuario con ese DNI");
                else
                    return BadRequest(inex.InnerException); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Users/5
        /// <summary>
        /// The DeleteUsers.
        /// </summary>
        /// <param name="dni">The id<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Users}}"/>.</returns>
        [HttpDelete("{dni}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Users>> DeleteUsers(string dni)
        {
            try
            {
                var users = _context.Users.Where(x=> x.DNI == dni).FirstOrDefault();
                if (users == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(users);
                await _context.SaveChangesAsync();

                return users;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
