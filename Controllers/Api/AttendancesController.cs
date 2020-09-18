using ChooseEvent2.DTOs;
using ChooseEvent2.Models;
using ChooseEvent2.Persistance;
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
        private IUnitOfWork unitOfWork;

        public AttendancesController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (unitOfWork.attendancesRepository
                .GetAttendance()
                .Any(e => e.GigId == dto.gigId && e.AttendeeId == userId))
            {
                return BadRequest("The attendance already exists");
            }
            var attendance = new Attendance()
            {
                GigId = dto.gigId,
                AttendeeId = userId
            };

            unitOfWork.attendancesRepository.AddAttendance(attendance);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult CancelAttendence(int id)
        {
            var userId = User.Identity.GetUserId();


            var attendance = unitOfWork.attendancesRepository
                .GetAttendance()
                .Single(e => e.GigId == id && e.AttendeeId == userId);

            if (attendance == null)
                return NotFound();

            unitOfWork.attendancesRepository.RemoveAttendance(attendance);
            unitOfWork.Complete();

            return Ok();
        }
    }

}
