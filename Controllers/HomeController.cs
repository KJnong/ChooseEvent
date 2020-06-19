using ChooseEvent2.Models;
using ChooseEvent2.ViewModels;
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
            var upcomingGigs = db.Gigs.Include(g => g.Artist).Include(g => g.Genre).Where(g => g.DateTime > DateTime.Now);

            var DisplayGigsOptions = new IndexGigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                Authorized = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs",DisplayGigsOptions);
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