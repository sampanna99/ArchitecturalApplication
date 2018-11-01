using System.Collections.Generic;
using ArchitecturalApplication.Models;

namespace ArchitecturalApplication.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        Gig GetGig(int id);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        void Add(Gig gig);
    }
}