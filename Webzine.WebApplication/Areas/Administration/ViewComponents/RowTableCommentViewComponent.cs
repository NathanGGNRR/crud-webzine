using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{

    public class RowTableComment : ViewComponent
    {


        public RowTableComment()
        {

        }

        public IViewComponentResult Invoke(CommentairesViewModel comment)
        {
            return View(comment);
        }

    }
}
