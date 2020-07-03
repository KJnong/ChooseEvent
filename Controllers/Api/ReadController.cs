using ChooseEvent2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace ChooseEvent2.Controllers.Api
{
    [Authorize]
    public class ReadController : ApiController
    {
        public ApplicationDbContext db;
        public ReadController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult IsRead()
        {

            var userId = User.Identity.GetUserId();

            var userNotifications = db.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            userNotifications.ForEach(userNotification => userNotification.Read());

            db.SaveChanges();
            return Ok();
        }
    }
}
