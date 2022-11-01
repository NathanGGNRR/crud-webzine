using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ArtistesController : Controller
    {

        #region Variables

        private IArtisteRepository _artisteRepository;
        private IConfiguration _configuration;
        private List<ArtisteViewModel> _artistes = new List<ArtisteViewModel>();
        private ArtisteViewModel _artiste;

        #endregion

        public ArtistesController(IArtisteRepository artisteRepository, IConfiguration configuration)
        {
            _artisteRepository = artisteRepository;
            _configuration = configuration;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);

            _artistes = _artisteRepository.Take(0, (_artisteRepository.Count() <= lengthPage) ? _artisteRepository.Count() : lengthPage).Select(vm => new ArtisteViewModel { IdArtiste = vm.IdArtiste, NomArtiste = vm.Nom }).ToList();

            IndexArtistesViewModel indexArtisteViewModel = new IndexArtistesViewModel
            {
                Artistes = _artistes,
                TotalArtistes = _artisteRepository.Count(),
                PageActuel = 1,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_artisteRepository.Count() - lengthPage > 0) ? true : false
            };

            return this.View(nameof(ArtistesController.Index), indexArtisteViewModel);
        }

        [HttpGet]
        public IActionResult Navigate(int id)
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            int indexActuel = (id - 1) * lengthPage;

            _artistes = _artisteRepository.Take(indexActuel, (_artisteRepository.Count() <= (id * lengthPage)) ? _artisteRepository.Count() - indexActuel : lengthPage).Select(vm => new ArtisteViewModel { IdArtiste = vm.IdArtiste, NomArtiste = vm.Nom }).ToList();

            IndexArtistesViewModel indexArtisteViewModel = new IndexArtistesViewModel
            {
                Artistes = _artistes,
                TotalArtistes = _artisteRepository.Count(),
                PageActuel = id,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_artisteRepository.Count() - indexActuel > lengthPage) ? true : false
            };

            return this.View(nameof(ArtistesController.Index), indexArtisteViewModel);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Artiste artiste = _artisteRepository?.Find(id);
                _artiste = new ArtisteViewModel
                {
                    IdArtiste = artiste.IdArtiste,
                    NomArtiste = artiste.Nom
                };
                return this.View(nameof(ArtistesController.Delete), _artiste);
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
            }
        }

        [HttpPost, ActionName(nameof(ArtistesController.Delete)), ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            try
            {
                _artisteRepository.Delete(_artisteRepository.Find(id));
                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
            }
        }
        #endregion

        #region Manage
        [HttpGet]
        public IActionResult Manage(int? id, string fonction)
        {
            if ((fonction == "Create" && !id.HasValue) || (fonction == "Edit" && id.HasValue))
            {
                try
                {
                    _artiste = new ArtisteViewModel();
                    if (id.HasValue)
                    {
                        Artiste artiste = _artisteRepository?.Find((int)id);
                        _artiste = new ArtisteViewModel
                        {
                            IdArtiste = artiste.IdArtiste,
                            Biographie = artiste.Biographie,
                            NomArtiste = artiste.Nom,
                        };
                    }

                    return this.View(nameof(ArtistesController.Manage), _artiste);
                }
                catch (NullReferenceException e)
                {
                    return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
                }
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPost, ActionName(nameof(ArtistesController.Manage)), ValidateAntiForgeryToken]
        public IActionResult ManagePOST(ArtisteViewModel artisteViewModel)
        {
            if (ModelState.IsValid)
            {
                Artiste artiste = new Artiste
                {
                    IdArtiste = (artisteViewModel.IdArtiste != 0) ? artisteViewModel.IdArtiste : 0,
                    Biographie = artisteViewModel.Biographie,
                    Nom = artisteViewModel.NomArtiste
                };

                if (artisteViewModel.IdArtiste != 0) { _artisteRepository.Update(artiste); } else { _artisteRepository.Add(artiste); }

                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
            }
            else
            {
                return this.View(nameof(ArtistesController.Manage), artisteViewModel);
            }
        }
        #endregion

    }
}
