using ArchitecturalApplication.Models;
using ArchitecturalApplication.Repositories;
using ArchitecturalApplication.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ArchitecturalApplication.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        private AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.DateTime > DateTime.Now && !a.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(a => a.Artist.Name.Contains(query) || a.Genre.Name.Contains(query) ||
                                                       a.Venue.Contains(query));
            }

            string userId = User.Identity.GetUserId();
            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}