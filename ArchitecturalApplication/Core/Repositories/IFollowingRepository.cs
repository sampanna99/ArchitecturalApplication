using ArchitecturalApplication.Core.Models;

namespace ArchitecturalApplication.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string ArtistId);
    }
}