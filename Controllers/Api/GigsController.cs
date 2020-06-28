using ChooseEvent2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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

            var gig = db.Gigs.Single(g => g.Id == Id && g.ArtistId == UserId);
            if (gig.IsCancelled)
            {
                return NotFound();
            }

            gig.IsCancelled = true;

            db.SaveChanges();

            return Ok();

        }
    }
}
