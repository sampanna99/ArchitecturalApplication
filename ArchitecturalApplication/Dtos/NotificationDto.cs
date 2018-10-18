using System;
using ArchitecturalApplication.Models;

namespace ArchitecturalApplication.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginaDateTime { get; set; }
        public string OriginalVenue { get; set; }

        public GigDto Gig { get; set; }

    }
}