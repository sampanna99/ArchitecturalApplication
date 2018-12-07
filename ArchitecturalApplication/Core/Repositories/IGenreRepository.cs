using System.Collections.Generic;
using ArchitecturalApplication.Core.Models;

namespace ArchitecturalApplication.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}