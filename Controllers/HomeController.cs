using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebHW_4.Models;

namespace WebHW_4.Controllers
{
    public class HomeController : Controller
    {
        FilmContext db;
        public HomeController(FilmContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var films = from m in db.Films
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                films = films.Where(s => s.name.Contains(searchString));
            }
            return View(await films.ToListAsync());
        }

        public ActionResult Info(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return RedirectToAction("Index");
            }
            return View(film);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Film film = db.Films.Find(id);

            if (film != null)
            {
                return View(film);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Film film)
        {
            db.Entry(film).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Film film)
        {
            if (db.Films.Contains(film))
            {
                return BadRequest();
            }
            db.Films.Add(film);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            Film film = db.Films.Find(id);

            if (film != null)
            {
                return View(film);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return NotFound();
            }
            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
