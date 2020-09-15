using ChooseEvent2.Models;
using ChooseEvent2.Persistance;
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
        private readonly IUnitOfWork unitOfWork;

        public HomeController()
        {
            db = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(db);
        }
        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs = unitOfWork.gigRepository.GetGigsWithArtistFolowweesGenreAndAttendances(userId);
            


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
                UserId = userId
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
