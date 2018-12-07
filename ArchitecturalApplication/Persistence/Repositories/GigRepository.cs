using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ArchitecturalApplication.Core.Models;
using ArchitecturalApplication.Core.Repositories;

namespace ArchitecturalApplication.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            var gigs = _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.Gig).Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
            return gigs;
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs.Include(a => a.Attendances.Select(l => l.Attendee)).SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled).Include(g => g.Genre)
                .ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}