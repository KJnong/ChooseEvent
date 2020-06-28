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
                Genres = db.Genres.ToList(),
                Heading = "Add Gig"

            };
            
            return View("GigForm", ViewModel);
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


            return RedirectToAction("Mine", "Gigs");
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
        public ActionResult ArtistFollowing()
        {
            var UserId = User.Identity.GetUserId();
            var artistFollowing = db.Relationships
                .Where(a => a.FolloweeId == UserId).Select(a => a.Follower).ToList();


            Console.WriteLine("Hellow ");
            return View(artistFollowing);
        }


        [Authorize]
        public ActionResult Mine()
        {
            var UserId = User.Identity.GetUserId();
            var gigs = db.Gigs
                .Where(g => g.ArtistId == UserId && g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var UserId = User.Identity.GetUserId();
            var gig = db.Gigs.Single(g => g.Id == id && g.ArtistId == UserId);

            var ViewModel = new GigsViewModel
            {
                Genres = db.Genres.ToList(),
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.Genreid,
                Venue = gig.Venue,
                Heading = "Edit Gig"
            };

            return View("GigForm", ViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigsViewModel viewModel)
        {
            var UserId = User.Identity.GetUserId();
            var gig = db.Gigs.Single(g => g.Id == viewModel.Id && g.ArtistId == UserId);


            gig.DateTime = viewModel.DateTime;
            gig.Genreid = viewModel.Genre;
            gig.Venue = viewModel.Venue;
           
            db.SaveChanges();


            return RedirectToAction("Mine", "Gigs");
        }


    }
}