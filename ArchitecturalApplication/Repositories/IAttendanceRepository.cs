using System.Collections.Generic;
using ArchitecturalApplication.Models;

namespace ArchitecturalApplication.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int gig, string userId);
    }
}