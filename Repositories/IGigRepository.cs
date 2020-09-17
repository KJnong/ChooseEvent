using ChooseEvent2.Models;
using System.Collections.Generic;

namespace ChooseEvent2.Repositories
{
    public interface IGigRepository
    {
        void AddGig(Gig gig);
        Gig ArtistGigWithArtistAndAttendances(int id);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> UserGig(string userId);
        IEnumerable<Gig> UserGigWithAttendee(string userId);
        IEnumerable<Gig> UserGigWithGenre(string userId);
        IEnumerable<Gig> GetGigsWithArtistFolowweesGenreAndAttendances(string userId);
        IEnumerable<Gig> GetGigWithAttendances();
    }
}