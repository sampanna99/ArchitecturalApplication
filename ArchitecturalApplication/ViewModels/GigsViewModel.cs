using ArchitecturalApplication.Models;
using System.Collections.Generic;

namespace ArchitecturalApplication.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}