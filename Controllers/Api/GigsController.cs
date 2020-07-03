using ChooseEvent2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChooseEvent2.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext db;

        public GigsController()
        {
            db = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int Id)
        {
            var UserId = User.Identity.GetUserId();

            var gig = db.Gigs.Include(a => a.Attendances.Select(g=>g.Attendee)).Single(g => g.Id == Id && g.ArtistId == UserId);
            if (gig.IsCancelled)
            {
                return NotFound();
            }

            gig.Cancel();

            db.SaveChanges();

            return Ok();

        }
    }
}
