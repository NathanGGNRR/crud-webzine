using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{

    public class PaginationArtistes : ViewComponent
    {


        public PaginationArtistes()
        {

        }

        public IViewComponentResult Invoke(IndexArtistesViewModel pagination)
        {
            return View(pagination);
        }
    }
}
