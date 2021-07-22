namespace API.Innovation.Controllers
{
    using API.Innovation.DGT.Infrastructure.Models;
    using API.Innovation.Infrastructure.Models;
    using API.Innovation.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="QueriesDgtController" />.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class QueriesDgtController : ControllerBase
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<QueriesDgtController> _logger;

        /// <summary>
        /// Defines the _queries.
        /// </summary>
        private readonly IUsersQueries _queries;

        /// <summary>
        /// Defines the _configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueriesDgtController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{QueriesDgt}"/>.</param>
        /// <param name="usersQueries">The usersQueries<see cref="IUsersQueries"/>.</param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public QueriesDgtController(ILogger<QueriesDgtController> logger,
            IUsersQueries usersQueries,
            IConfiguration configuration)
        {
            this._queries = usersQueries ?? throw new ArgumentNullException(nameof(usersQueries));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// The GetUserLimitAsync.
        /// </summary>
        /// <param name="limit">The limit<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet]
        [Route("userTop/{top:range(1,9223372036854775807)}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Users>>> GetUserLimitAsync(int top)
        {
            try
            {
                var users = await this._queries.GetTopUserAsync(this._configuration.GetValue<string>("ConnectionString"), top);
                if (users.Count() > 0)
                    return Ok(users);
                else
                    return BadRequest("lista de usuarios vacia");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// The GetHistoryUserAsync.
        /// </summary>
        /// <param name="dni">The dni<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpGet]
        [Route("History")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<HistoryUserModel>>> GetHistoryUserAsync(string dni)
        {
            try
            {
                var historyUserModel = await this._queries.GetHistoryUser(this._configuration.GetValue<string>("ConnectionString"), dni);
                if (historyUserModel.Count() > 0)
                    return Ok(historyUserModel);
                else
                    return BadRequest("lista de usuarios vacia");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
