using System;
using System.Collections.Generic;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class IndexTitresViewModel
    {
        public List<TitresViewModel> Titres { get; set; }
        public int TotalTitres { get; set; }
        public int PageActuel { get; set; }
        public int LengthPage { get; set; }
        public int DisplayPage { get; set; }
        public Boolean Next { get; set; }

    }
}
