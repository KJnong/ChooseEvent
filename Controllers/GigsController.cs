using ChooseEvent2.Models;
using ChooseEvent2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChooseEvent2.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext db;
        public GigsController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Gigs
        public ActionResult Create()
        {
            var ViewModel = new GigsViewModel
            {
                Genres = db.Genres.ToList()
            };
            
            return View(ViewModel);
        }
    }
}