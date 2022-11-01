using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.ViewModels;

namespace Webzine.WebApplication.Controllers
{
    public class ArtisteController : Controller
    {

        #region Variables
        private IArtisteRepository _artisteRepository;
        #endregion

        public ArtisteController(IArtisteRepository artisteRepository)
        {
            _artisteRepository = artisteRepository;
        }

        #region Index
        [HttpGet]
        public IActionResult Index(string id)
        {
            var artisteVM = new ArtisteViewModel();
            if (id.Contains("%2F"))
            {
                id = id.Replace("%2F", "/");
            }
            artisteVM.Artist = _artisteRepository.Find(id);
            artisteVM.Albums = artisteVM.Artist.Titres.GroupBy(a => a.Album).Select(grp => grp.ToList()).ToList();

            return this.View(artisteVM);
        }
        #endregion
    }
}
