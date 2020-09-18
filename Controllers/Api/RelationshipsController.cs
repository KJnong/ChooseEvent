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
    public class RelationshipsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public RelationshipsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(RelationshipsDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (unitOfWork.relationshipRepository.GetRelationships().Any(e => e.FolloweeId == userId && e.FollowerId == dto.FollowerId))
            {
                return BadRequest("The relationships already exists");
            }
            var relationship = new Relationship()
            {
                FolloweeId = userId,
                FollowerId = dto.FollowerId
            };

            unitOfWork.relationshipRepository.AddRelationship(relationship);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();

             var relationship = unitOfWork.relationshipRepository.GetRelationships().Single(e => e.FolloweeId == userId && e.FollowerId == id);




            unitOfWork.relationshipRepository.RemoveRelationship(relationship);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
