using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{
    public class RowTableStyle : ViewComponent
    {
        public RowTableStyle()
        {

        }

        public IViewComponentResult Invoke(StylesViewModel style)
        {
            return View(style);
        }
    }
}
