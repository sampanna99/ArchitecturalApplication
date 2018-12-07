using System.Collections.Generic;
using System.Linq;
using ArchitecturalApplication.Core.Models;
using ArchitecturalApplication.Core.Repositories;

namespace ArchitecturalApplication.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }


    }
}