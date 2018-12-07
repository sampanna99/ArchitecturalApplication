using System;
using ArchitecturalApplication.Core.Models;

namespace ArchitecturalApplication.Core.Dtos
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