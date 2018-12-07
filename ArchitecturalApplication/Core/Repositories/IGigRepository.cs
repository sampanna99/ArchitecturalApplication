using System.Collections.Generic;
using ArchitecturalApplication.Core.Models;

namespace ArchitecturalApplication.Core.Repositories
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