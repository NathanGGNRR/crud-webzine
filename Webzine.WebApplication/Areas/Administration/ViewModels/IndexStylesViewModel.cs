using System;
using System.Collections.Generic;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class IndexStylesViewModel
    {
        public List<StylesViewModel> Styles { get; set; }
        public int TotalStyles { get; set; }
        public int PageActuel { get; set; }
        public int LengthPage { get; set; }
        public int DisplayPage { get; set; }
        public Boolean Next { get; set; }

    }
}
