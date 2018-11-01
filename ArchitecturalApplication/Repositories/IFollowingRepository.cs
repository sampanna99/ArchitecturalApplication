using ArchitecturalApplication.Models;

namespace ArchitecturalApplication.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string ArtistId);
    }
}