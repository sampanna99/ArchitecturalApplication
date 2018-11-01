using ArchitecturalApplication.Models;
using ArchitecturalApplication.Persistence;
using ArchitecturalApplication.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace ArchitecturalApplication.Controllers
{
    public class GigsController : Controller
    {
        //private readonly AttendanceRepository _attendanceRepository;
        //private readonly GigRepository _gigRepository;
        //private readonly FollowingRepository _followingRepository;
        //private readonly GenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            //_attendanceRepository = new AttendanceRepository(_context);
            //_gigRepository = new GigRepository(_context);
            //_followingRepository = new FollowingRepository(_context);
            //_genreRepository = new GenreRepository(_context);
            _unitOfWork = unitOfWork;
            //_unitOfWork = new UnitOfWork(new ApplicationDbContext());

        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add s Gig"
            };

            return View("GigForm", viewModel);
        }


        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }


        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);
            if (gig == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;
                //viewModel.IsAttending = _context.Attendances.Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.IsFollowing =
                    _unitOfWork.Followings.GetFollowing(userId, gig.ArtistId) != null;
                //viewModel.IsFollowing =
                //    _context.Followings.Any(a => a.FolloweeId == gig.ArtistId && a.FolloweeId == userId);
            }
            return View("Details", viewModel);
        }




        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGig(id);
            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId != userId)
            {
                return new HttpUnauthorizedResult();
            }

            //var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Id = gig.Id,
                Heading = "Edit a Gig"

            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();


            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };
            return View("Gigs", viewModel);
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            //_context.Gigs.Add(gig);
            //_context.SaveChanges();
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = new ApplicationDbContext().Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);


            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);
            return View(gigs);
        }

    }
}