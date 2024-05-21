using Microsoft.AspNetCore.Mvc;
using MvcCoreSeriesBeanstalk.Models;
using MvcCoreSeriesBeanstalk.Repositories;

namespace MvcCoreSeriesBeanstalk.Controllers
{
    public class SeriesController : Controller
    {
        private RepositorySeries repo;

        public SeriesController(RepositorySeries repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.repo.GetSeriesAsync();
            return View(series);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Serie serie)
        {
            await this.repo.CreateSerieAsync(serie.Nombre, serie.Imagen, serie.Anyo);
            return RedirectToAction("Index");
        }
    }
}
