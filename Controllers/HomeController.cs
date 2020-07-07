using ChooseEvent2.Models;
using ChooseEvent2.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index(string query = null)
        {
            var UserId = User.Identity.GetUserId();

            var upcomingGigs = db.Gigs
                .Include(g => g.Artist.Followees)
                .Include(g => g.Genre)
                .Include(g => g.Attendances)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);


            if (!String.IsNullOrEmpty(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(query) ||
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query));
            }


            var DisplayGigsOptions = new IndexGigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                Authorized = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                Search = query,
                UserId = UserId
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