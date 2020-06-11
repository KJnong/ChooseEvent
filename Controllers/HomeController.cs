using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChooseEvent2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = db.Gigs.Include(g => g.Artist).Where(g => g.DateTime > DateTime.Now);
            return View(upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}