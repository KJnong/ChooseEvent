using ChooseEvent2.Models;
using ChooseEvent2.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
      
        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new GigsViewModel
            {
                Genres = db.Genres.ToList()
            };
            
            return View(ViewModel);
        }

        [Authorize]
        public ActionResult GigsAttending()
        {
            var UserId = User.Identity.GetUserId();
            var gigsAttending = db.Attendances
                .Where(a => a.AttendeeId == UserId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();


            var Gigs = new IndexGigsViewModel
            {
                Authorized = User.Identity.IsAuthenticated,
                UpcomingGigs = gigsAttending,
                Heading = "Atending"
            };
            return View("Gigs", Gigs);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigsViewModel viewModel)
        {
         
            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.DateTime,
                Genreid = viewModel.Genre,
                Venue = viewModel.Venue
            };

            db.Gigs.Add(gig);
            db.SaveChanges();


            return RedirectToAction("Index", "Home");
        }
    }
}