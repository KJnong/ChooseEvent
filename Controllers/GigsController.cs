 using ChooseEvent2.Models;
using ChooseEvent2.Persistance;
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
        private readonly IUnitOfWork unitOfWork;

        public GigsController()
        {
            unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }
      
        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new GigsViewModel
            {
                Genres = unitOfWork.genreRepository.Genres(),
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

            unitOfWork.gigRepository.AddGig(gig);
            unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult GigsAttending()
        {
            var UserId = User.Identity.GetUserId();

            var gigsAttending = unitOfWork.gigRepository.GetGigsUserAttending(UserId);
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

            var artistFollowing = unitOfWork.applicationUserRepository.ArtistFollowing(userId);
            return View(artistFollowing);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = unitOfWork.gigRepository.UserGigWithGenre(userId);
            return View(gigs);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = unitOfWork.gigRepository.UserGig(userId).Single(g => g.Id == id);
                
            var ViewModel = new GigsViewModel
            {
                Genres = unitOfWork.genreRepository.Genres(),
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

            var gig = unitOfWork.gigRepository.UserGigWithAttendee(userId).Single(g => g.Id == viewModel.Id);
             
            var originalDateTime = gig.DateTime;
            var originalVenue = gig.Venue;

            gig.Update(viewModel, originalDateTime, originalVenue);

            unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Info(int id)
        {
            var gig = unitOfWork.gigRepository.ArtistGigWithArtistAndAttendances(id);

            if (gig == null)
                return HttpNotFound();

            var UserId = User.Identity.GetUserId();

            var info = new InfoViewModel
            {
                Artist = gig.Artist,
                Vanue = gig.Venue,
                DateTime = gig.DateTime,
                relationship = unitOfWork.relationshipRepository.GetRelationships(),
                UserId = UserId
            };

            if (gig.Attendances.Any(a => a.AttendeeId == UserId && a.GigId == id))
            {
                info.Attending = true;
            }

            if (unitOfWork.relationshipRepository.GetRelationships().Any(f =>f.FollowerId == gig.ArtistId && f.FolloweeId == UserId))
            {
                info.Following = true;
            }

            return View(info);
        }
 
    }

}