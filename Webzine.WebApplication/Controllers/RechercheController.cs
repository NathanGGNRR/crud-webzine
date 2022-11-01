using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.ViewModels;

namespace Webzine.WebApplication.Controllers
{
    public class RechercheController : Controller
    {

        #region Variables
        private IArtisteRepository _artisteRepository;
        private ITitreRepository _titreRepository;
        #endregion

        public RechercheController(IArtisteRepository artisteRepository, ITitreRepository titreRepository)
        {
            _artisteRepository = artisteRepository;
            _titreRepository = titreRepository;
        }

        #region Index
        public IActionResult Index(string keywords)
        {
            var titres = _titreRepository.Search(keywords).ToList();
            var artistes = _artisteRepository.Search(keywords).ToList();

            var model = new RechercheViewModel { Keywords = keywords, Artistes = artistes, Titres = titres };

            return this.View(model);
        }

        #endregion
    }
}