using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{

    public class PaginationTitres : ViewComponent
    {


        public PaginationTitres()
        {

        }

        public IViewComponentResult Invoke(IndexTitresViewModel pagination)
        {
            return View(pagination);
        }
    }
}
