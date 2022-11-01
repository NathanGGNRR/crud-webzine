using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.WebApplication.ViewComponents
{

    public class AlbumCards : ViewComponent
    {


        public AlbumCards()
        {

        }

        public IViewComponentResult Invoke(List<Titre> tracks)
        {
            return View(tracks);
        }
    }
}
