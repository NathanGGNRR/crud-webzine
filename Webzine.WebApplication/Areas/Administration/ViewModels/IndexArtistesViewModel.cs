using System;
using System.Collections.Generic;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class IndexArtistesViewModel
    {
        public List<ArtisteViewModel> Artistes { get; set; }
        public int TotalArtistes { get; set; }
        public int PageActuel { get; set; }
        public int LengthPage { get; set; }
        public int DisplayPage { get; set; }
        public Boolean Next { get; set; }

    }
}
