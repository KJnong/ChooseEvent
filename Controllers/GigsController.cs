 using ChooseEvent2.Models;
using ChooseEvent2.Repositories;
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
        private readonly GigRepository gigRepository;
        private readonly ApplicationUserRepository applicationUserRepository;
        private readonly GenreRepository genreRepository;
        private readonly ApplicationDbContext db;
        public GigsController()
        {
            db = new ApplicationDbContext();
            gigRepository = new GigRepository(db);
            applicationUserRepository = new ApplicationUserRepository(db);
            genreRepository = new GenreRepository(db);
        }
      
        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new GigsViewModel
            {
                Genres = genreRepository.Genres(),
                Heading = "Add Gig"
            };
            return View("GigForm", ViewModel);
        }

        [HttpPost]
        public ActionResult Search(IndexGigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.Search });
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

            var gigsAttending = gigRepository.GetGigsUserAttending(UserId);
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
            var userId = User.Identity.GetUserId();

            var artistFollowing = applicationUserRepository.ArtistFollowing(userId);
            return View(artistFollowing);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = UserGigsWithGenre(userId);
            return View(gigs);
        }

        private List<Gig> UserGigsWithGenre(string userId)
        {
            var gigs = db.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();

            return gigs;
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = gigRepository.UserGig(userId).Single(g => g.Id == id);
                
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
            var userId = User.Identity.GetUserId();

            var gig = gigRepository.UserGigWithAttendee(userId).Single(g => g.Id == viewModel.Id);
             
            var originalDateTime = gig.DateTime;
            var originalVenue = gig.Venue;

            gig.Update(viewModel, originalDateTime, originalVenue);
           
            db.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Info(int id)
        {
            var gig = gigRepository.ArtistGigWithArtistAndAttendances(id);

            if (gig == null)
                return HttpNotFound();

            var UserId = User.Identity.GetUserId();

            var info = new InfoViewModel
            {
                Artist = gig.Artist,
                Vanue = gig.Venue,
                DateTime = gig.DateTime,
                relationship = db.Relationships.ToList(),
                UserId = UserId
            };

            if (gig.Attendances.Any(a => a.AttendeeId == UserId && a.GigId == id))
            {
                info.Attending = true;
            }

            if (db.Relationships.Any(f =>f.FollowerId == gig.ArtistId && f.FolloweeId == UserId))
            {
                info.Following = true;
            }

            return View(info);
        }
 
    }

}