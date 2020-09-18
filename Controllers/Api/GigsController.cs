using ChooseEvent2.Models;
using ChooseEvent2.Persistance;
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
        private IUnitOfWork unitOfWork;

        public GigsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int Id)
        {
            var UserId = User.Identity.GetUserId();

            var gig = unitOfWork.gigRepository.GetGigWithAttendances().Single(g => g.Id == Id && g.ArtistId == UserId);
            if (gig.IsCancelled)
            {
                return NotFound();
            }

            gig.Cancel();

            unitOfWork.Complete();

            return Ok();

        }
    }
}
