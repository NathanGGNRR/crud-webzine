using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{
    public class RowTableTitre : ViewComponent
    {
        public RowTableTitre()
        {

        }

        public IViewComponentResult Invoke(TitresViewModel titre)
        {
            return View(titre);
        }
    }
}
