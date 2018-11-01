using System.Collections.Generic;
using ArchitecturalApplication.Models;

namespace ArchitecturalApplication.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}