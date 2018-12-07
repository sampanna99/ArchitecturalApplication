using ArchitecturalApplication.Core.Dtos;
using ArchitecturalApplication.Core.Models;
using ArchitecturalApplication.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace ArchitecturalApplication.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(a => a.FollowerId == userId && a.FolloweeId == dto.Id))
            {
                return BadRequest("Following already exists");
            }

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.Id
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings.SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == id);

            if (following == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(following);
            _context.SaveChanges();
            return Ok(id);
        }

    }
}
