using ArchitecturalApplication.Dtos;
using ArchitecturalApplication.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace ArchitecturalApplication.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications.Where(a => a.UserId == userId && !a.IsRead).Select(u => u.Notification)
                .Include(n => n.Gig.Artist).ToList();


            return notifications.Select(Mapper.Map<Notification, NotificationDto>);

            //return notifications.Select(a => new NotificationDto()
            //{
            //    DateTime = a.DateTime,
            //    Gig = new GigDto
            //    {
            //        Artist = new UserDto
            //        {
            //            Id = a.Gig.Artist.Id,
            //            Name = a.Gig.Artist.Name
            //        },
            //        DateTime = a.Gig.DateTime,
            //        Id = a.Gig.Id,
            //        IsCanceled = a.Gig.IsCanceled,
            //        Venue = a.Gig.Venue
            //    },
            //    OriginaDateTime = a.OriginaDateTime,
            //    OriginalVenue = a.OriginalVenue,
            //    Type = a.Type
            //});
        }

        public IHttpActionResult markAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications.Where(a => a.UserId == userId && !a.IsRead);
            notifications.ForEach(a => a.Read());
            _context.SaveChanges();
            return Ok();
        }
    }
}
