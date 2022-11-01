using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class ManageTitresViewModel
    {
        public TitresViewModel Titre { get; set; }
        public SelectList Artistes { get; set; }
        public List<StylesViewModel> Styles { get; set; }

    }


}
