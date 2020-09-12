using ChooseEvent2.Models;
using System.Collections.Generic;

namespace ChooseEvent2.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> ArtistFollowing(string userId);
    }
}