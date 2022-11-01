using Microsoft.AspNetCore.Mvc;
using Webzine.Repository.Contracts;

namespace Webzine.WebApplication.ViewComponents
{
    public class StylesSidebarViewComponent : ViewComponent
    {
        private IStyleRepository _styleRepo;

        public StylesSidebarViewComponent(IStyleRepository repo)
        {
            _styleRepo = repo;
        }

        public IViewComponentResult Invoke()
        {
            return View(_styleRepo.FindAll());
        }
    }
}
