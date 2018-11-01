using ArchitecturalApplication.Models;
using System.Linq;

namespace ArchitecturalApplication.Repositories
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