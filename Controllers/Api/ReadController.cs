using ChooseEvent2.Models;
using ChooseEvent2.Persistance;
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
        private readonly IUnitOfWork unitOfWork;
        public ReadController(IUnitOfWork _unitOfWork)
        { 
            unitOfWork = _unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult IsRead()
        {

            var userId = User.Identity.GetUserId();

            var userNotifications = unitOfWork.userNotificationRepository.GetUserNotifications(userId);

            userNotifications.ForEach(userNotification => userNotification.Read());

            unitOfWork.Complete();
            return Ok();
        }
    }
}
