using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{

    public class PaginationCommentaires : ViewComponent
    {


        public PaginationCommentaires()
        {

        }

        public IViewComponentResult Invoke(IndexCommentairesViewModel pagination)
        {
            return View(pagination);
        }
    }
}
