namespace API.Innovation.Controllers
{
    using API.Innovation.Application.Command;
    using API.Innovation.Infrastructure;
    using API.Innovation.Infrastructure.Models;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CarsController" />.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarsController : ControllerBase
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
        /// Initializes a new instance of the <see cref="CarsController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="DbAPIContext"/>.</param>
        /// <param name="mediat">The mediat<see cref="IMediator"/>.</param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public CarsController(DbAPIContext context, IMediator mediat)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._mediator = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

       /// <summary>
        /// The GetCars.
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{Cars}}}"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cars>>> GetCars()
        {
            return await this._context.Cars.ToListAsync();
        }

        /// <summary>
        /// The GetCars.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Cars}}"/>.</returns>
        [HttpGet("{matricula}")]
        public ActionResult<Cars> GetCars(string matricula)
        {
            var cars = this._context.Cars.Where(x=> x.Matricula == matricula).FirstOrDefault();

            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
        }

        /// <summary>
        /// The PutCars.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="cars">The cars<see cref="Cars"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPut("{matricula}")]
        public async Task<IActionResult> PutCars(string matricula, Cars cars)
        {
            if (matricula != cars.Matricula)
            {
                return BadRequest();
            }
            var createCars = new UpdateCarCommand(cars);
            var command = await _mediator.Send(createCars);

            if (command)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// The PostCars.
        /// </summary>
        /// <param name="cars">The cars<see cref="Cars"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Cars}}"/>.</returns>
        [HttpPost]
        public async Task<ActionResult<Cars>> PostCars(Cars cars)
        {
            try
            {
                var createCars = new CreateCarCommand(cars);
                var command = await _mediator.Send(createCars);
                return Ok();
            }
            catch (IndexOutOfRangeException err)
            {
                return Conflict(err.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// The DeleteCars.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult{Cars}}"/>.</returns>
        [HttpDelete("{matricula}")]
        public async Task<ActionResult<Cars>> DeleteCars(string matricula)
        {
            var car = this._context.Cars.Where(x => x.Matricula == matricula).FirstOrDefault();
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

  
    }
}
