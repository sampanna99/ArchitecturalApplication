using ArchitecturalApplication.Dtos;
using ArchitecturalApplication.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace ArchitecturalApplication.Controllers
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
    }
}
