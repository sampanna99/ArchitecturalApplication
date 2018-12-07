using System.Collections.Generic;
using ArchitecturalApplication.Core.Models;

namespace ArchitecturalApplication.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int gig, string userId);
    }
}