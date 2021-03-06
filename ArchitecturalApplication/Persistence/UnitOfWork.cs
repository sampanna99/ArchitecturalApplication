﻿using ArchitecturalApplication.Core;
using ArchitecturalApplication.Core.Repositories;
using ArchitecturalApplication.Persistence.Repositories;

namespace ArchitecturalApplication.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository Gigs { get; private set; }

        public IGenreRepository Genres { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IAttendanceRepository Attendances { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Followings = new FollowingRepository(context);
            Attendances = new AttendanceRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}