using ChooseEvent2.DTOs;
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
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext db;

        public AttendancesController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (db.Attendances.Any(e => e.GigId == dto.gigId && e.AttendeeId == userId))
            {
                return BadRequest("The attendance already exists");
            }
            var attendance = new Attendance()
            {
                GigId = dto.gigId,
                AttendeeId = userId
            };

            db.Attendances.Add(attendance);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult CancelAttendence(int id)
        {
            var userId = User.Identity.GetUserId();


            var attendance = db.Attendances.Single(e => e.GigId == id && e.AttendeeId == userId);

            if (attendance == null)
                return NotFound();

            db.Attendances.Remove(attendance);
            db.SaveChanges();

            return Ok();
        }
    }

}
