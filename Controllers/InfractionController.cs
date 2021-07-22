namespace API.Innovation.Controllers
{
    using API.Innovation.Infrastructure;
    using API.Innovation.Infrastructure.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="InfractionController" />.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InfractionController : ControllerBase
    {
        /// <summary>
        /// Defines the _context.
        /// </summary>
        private readonly DbAPIContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfractionController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="DbAPIContext"/>.</param>
        public InfractionController(DbAPIContext context)
        {
            _context = context;
        }

        // GET: api/Infringements
        /// <summary>
        /// The GetInfringement.
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{Infraction}}}"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Infraction>>> GetInfraction()
        {
            return await _context.Infraction.ToListAsync();
        }

        /// <summary>
        /// The GetInfringement.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Infringement}}"/>.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Infraction>> GetInfraction(int id)
        {
            var infringement = await _context.Infraction.FindAsync(id);

            if (infringement == null)
            {
                return NotFound();
            }

            return infringement;
        }

        /// <summary>
        /// The PutInfringement.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="infringement">The infringement<see cref="Infringement"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfraction(int id, Infraction infringement)
        {
            if (id != infringement.IdInfraction)
            {
                return BadRequest();
            }

            _context.Entry(infringement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfractionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // <summary>
        /// The PostInfringement.
        /// </summary>
        /// <param name="infringement">The infringement<see cref="Infraction"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Infraction}}"/>.</returns>
        [HttpPost]
        public async Task<ActionResult<Infraction>> PostInfraction(Infraction infringement)
        {
            _context.Infraction.Add(infringement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfraction", new { id = infringement.IdInfraction }, infringement);
        }

        /// <summary>
        /// The DeleteInfringement.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Infringement}}"/>.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Infraction>> DeleteInfraction(int id)
        {
            var infringement = await _context.Infraction.FindAsync(id);
            if (infringement == null)
            {
                return NotFound();
            }

            _context.Infraction.Remove(infringement);
            await _context.SaveChangesAsync();

            return infringement;
        }

        /// <summary>
        /// The InfringementExists.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool InfractionExists(int id)
        {
            return _context.Infraction.Any(e => e.IdInfraction == id);
        }
    }
}
