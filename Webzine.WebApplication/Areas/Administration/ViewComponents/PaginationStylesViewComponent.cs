using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{

    public class PaginationStyles : ViewComponent
    {


        public PaginationStyles()
        {

        }

        public IViewComponentResult Invoke(IndexStylesViewModel pagination)
        {
            return View(pagination);
        }
    }
}
