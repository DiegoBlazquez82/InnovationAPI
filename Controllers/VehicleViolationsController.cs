namespace API.Innovation.Controllers
{
    using API.Innovation.DGT.Infrastructure.Models;
    using API.Innovation.DGT.Infrastructure.Queries;
    using API.Innovation.Infrastructure;
    using API.Innovation.Infrastructure.Models;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="VehicleViolationsController" />.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleViolationsController : ControllerBase
    {
        /// <summary>
        /// Defines the _context.
        /// </summary>
        private readonly DbAPIContext _context;

        /// <summary>
        /// Defines the _mediat.
        /// </summary>
        private readonly IMediator _mediat;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleViolationsController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="DbAPIContext"/>.</param>
        /// <param name="mediat">The mediat<see cref="IMediator"/>.</param>
        public VehicleViolationsController(DbAPIContext context, IMediator mediat)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        // GET: api/VehicleViolations
        /// <summary>
        /// The GetVehicleViolation.
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{VehicleViolation}}}"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleViolation>>> GetVehicleViolation()
        {
            return await _context.VehicleViolation.ToListAsync();
        }

        // GET: api/VehicleViolations/5
        /// <summary>
        /// The GetVehicleViolation.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{VehicleViolation}}"/>.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleViolation>> GetVehicleViolation(int id)
        {
            var vehicleViolation = await _context.VehicleViolation.FindAsync(id);

            if (vehicleViolation == null)
            {
                return NotFound();
            }

            return vehicleViolation;
        }

        /// <summary>
        /// The PutVehicleViolation.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="vehicleViolation">The vehicleViolation<see cref="VehicleViolation"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleViolation(int id, VehicleViolation vehicleViolation)
        {
            if (id != vehicleViolation.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleViolation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleViolationExists(id))
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

        /// <summary>
        /// The PostVehicleViolation.
        /// </summary>
        /// <param name="vehicleViolation">The vehicleViolation<see cref="AddInfractionCarModel"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{VehicleViolation}}"/>.</returns>
        [HttpPost]
        public async Task<ActionResult<VehicleViolation>> PostVehicleViolation(AddInfractionCarModel vehicleViolation)
        {
            try
            {
                var addvehicleViolation = new AddPenaltyCommand(vehicleViolation);
                var command = await this._mediat.Send(addvehicleViolation);
                if (command)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// The DeleteVehicleViolation.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{VehicleViolation}}"/>.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleViolation>> DeleteVehicleViolation(int id)
        {
            var vehicleViolation = await _context.VehicleViolation.FindAsync(id);
            if (vehicleViolation == null)
            {
                return NotFound();
            }

            _context.VehicleViolation.Remove(vehicleViolation);
            await _context.SaveChangesAsync();

            return vehicleViolation;
        }

        /// <summary>
        /// The VehicleViolationExists.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool VehicleViolationExists(int id)
        {
            return _context.VehicleViolation.Any(e => e.Id == id);
        }
    }
}
