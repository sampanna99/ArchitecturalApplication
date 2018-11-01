using ArchitecturalApplication.Repositories;

namespace ArchitecturalApplication.Persistence
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        IAttendanceRepository Attendances { get; set; }
        void Complete();
    }
}