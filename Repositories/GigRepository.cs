﻿using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChooseEvent2.Repositories
{
    public class GigRepository
    {
        private readonly ApplicationDbContext db;
        public GigRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IEnumerable<Gig> UserGigWithAttendee(string userId)
        {
            var gig = db.Gigs
                .Include(a => a.Attendances
                .Select(g => g.Attendee))
                .Where(g => g.ArtistId == userId)
                .ToList();

            return gig;
        }

        public Gig ArtistGigWithArtistAndAttendances(int id)
        {
            var gig = db.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Attendances)
                    .SingleOrDefault(g => g.Id == id);

            return gig;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            var gigsAttending = db.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();

            return gigsAttending;
        }

        public IEnumerable<Gig> UserGig(string userId)
        {
            var gig = db.Gigs
                .Where(g => g.ArtistId == userId)
                .ToList();
            return gig;
        }

        public void AddGig(Gig gig)
        {
            db.Gigs.Add(gig);
        }
    }
}