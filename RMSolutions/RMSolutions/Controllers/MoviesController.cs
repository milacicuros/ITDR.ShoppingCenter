using Microsoft.AspNetCore.Mvc;
using RMSolutions.Models;

namespace RMSolutions.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek!" };
                
            return View(movie);
        }
    }
}