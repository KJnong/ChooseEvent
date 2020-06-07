﻿using ChooseEvent2.Models;
using ChooseEvent2.ViewModels;
using Microsoft.AspNet.Identity;
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
        [HttpPost]
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