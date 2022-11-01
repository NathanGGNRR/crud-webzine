using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{

    public class RowTableArtiste : ViewComponent
    {


        public RowTableArtiste()
        {

        }

        public IViewComponentResult Invoke(ArtisteViewModel artiste)
        {
            return View(artiste);
        }
    }
}
