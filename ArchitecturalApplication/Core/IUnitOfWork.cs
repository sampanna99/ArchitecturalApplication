using ArchitecturalApplication.Core.Repositories;

namespace ArchitecturalApplication.Core
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