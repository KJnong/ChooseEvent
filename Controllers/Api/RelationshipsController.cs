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
    public class RelationshipsController : ApiController
    {
        private readonly ApplicationDbContext db;

        public RelationshipsController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(RelationshipsDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (db.Relationships.Any(e => e.FolloweeId == userId && e.FollowerId == dto.FollowerId))
            {
                return BadRequest("The relationships already exists");
            }
            var relationship = new Relationship()
            {
                FolloweeId = userId,
                FollowerId = dto.FollowerId
            };

            db.Relationships.Add(relationship);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult UnFollow(RelationshipsDto dto)
        {
            var userId = User.Identity.GetUserId();

             var relationship = db.Relationships.Single(e => e.FolloweeId == userId && e.FollowerId == dto.FollowerId);
            
           

            db.Relationships.Remove(relationship);
            db.SaveChanges();

            return Ok();
        }
    }
}
