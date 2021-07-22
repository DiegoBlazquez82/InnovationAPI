namespace API.Innovation.Controller
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="HomeController" />.
    /// </summary>
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
