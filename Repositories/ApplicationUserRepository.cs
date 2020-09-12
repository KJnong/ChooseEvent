using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext db;
        public ApplicationUserRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IEnumerable<ApplicationUser> ArtistFollowing(string userId)
        {
            var artistFollowing = db.Relationships
               .Where(a => a.FolloweeId == userId)
               .Select(a => a.Follower)
               .ToList();

            return artistFollowing;
        }
    }
}