using System.Linq;
using ArchitecturalApplication.Core.Models;
using ArchitecturalApplication.Core.Repositories;

namespace ArchitecturalApplication.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Following GetFollowing(string userId, string ArtistId)
        {
            return _context.Followings.SingleOrDefault(a => a.FolloweeId == ArtistId && a.FollowerId == userId);
        }

    }
}