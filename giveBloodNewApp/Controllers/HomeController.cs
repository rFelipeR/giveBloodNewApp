using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace giveBloodNewApp.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment hostingEnvironment;

        public HomeController(IHostingEnvironment env)
        {
            hostingEnvironment = env;
        }
        public IActionResult Index(int? yearFilter, int? gerneIdFilter) //opcional ?, pode ser Nullable<int>
        {
            context schedulesContext = new SchedulesContext();   //iniciar context 1º

            var movieList = schedulesContext.Movies.OrderByDescending(x => x.Name)

                //.Where(x => x.Picture != "" && x.Picture != null);
                .Where(x => true);

            if (yearFilter.HasValue)
            {
                movieList = movieList.Where(x => x.Year == yearFilter);
            }

            if (gerneIdFilter.HasValue)
            {
                movieList = movieList.Where(x => x.GenreId == gerneIdFilter);
            }

            ViewBag.Genres = schedulesContext.Genres.OrderBy(x => x.Name).ToList();
            return View(movieList.ToList());
        }

        public IActionResult Detail(int id)
        {

            MoviesContext moviesContext = new MoviesContext();
            Movie movie = moviesContext.Movies.Find(id);
            //moviesContext.Movies.Where(x => x.Id == id).FirstOrDefault();

            return View(movie);
        }

        public IActionResult New()
        {
            MoviesContext moviesContext = new MoviesContext();
            ViewBag.Genres = moviesContext.Genres.OrderBy(x => x.Name).ToList();
            Movie movie = new Movie();
            return View(movie);
        }
        [HttpPost]
        public IActionResult New(Movie movie, FormFile PictureUpload)
        {
            MoviesContext moviesContext = new MoviesContext();

            if (PictureUpload != null && PictureUpload.Length > 0)
            {
                string caminhoArquivo = Path.GetTempFileName();
                string caminho_WebRoot = hostingEnvironment.WebRootPath;
            }

            moviesContext.Movies.Add(movie);
            moviesContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
