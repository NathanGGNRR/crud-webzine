using Microsoft.AspNetCore.Mvc;
using System;
using Webzine.Business.Contracts;
using Webzine.Entity;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.ViewModels;

namespace Webzine.WebApplication.Controllers
{
    public class TitreController : Controller
    {
        #region Variables
        private ITitreRepository _titreRepository;
        private ICommentaireRepository _commentaireRepository;
        private ITitreBusiness _titreBusiness;
        private TitreViewModel _commentaire { get; set; }
        #endregion

        public TitreController(ITitreRepository titreRepository, ICommentaireRepository commentaireRepository, ITitreBusiness titreBusiness)
        {
            _titreRepository = titreRepository;
            _commentaireRepository = commentaireRepository;
            _titreBusiness = titreBusiness;
        }

        #region Index
        [HttpGet]
        public IActionResult Index(int id)
        {
            Titre titre = _titreRepository.Find(id);
            TitreViewModel titreViewModel = new TitreViewModel
            {
                Titre = titre,
                IdTitre = id
            };
            _titreBusiness.IncrementVue(titre);
            return View(titreViewModel);
        }

        [HttpGet]
        public IActionResult titreFromArtist(int id, string artiste, string titre)
        {
            TitreViewModel titreViewModel = new TitreViewModel
            {
                Titre = _titreRepository.Find(id),
                IdTitre = id
            };
            return View(nameof(TitreController.Index), titreViewModel);
        }
        #endregion

        #region Like
        [HttpPost]
        public IActionResult Like(int id)
        {
            Titre titre = _titreRepository.Find(id);
            TitreViewModel titreViewModel = new TitreViewModel
            {
                Titre = titre,
                IdTitre = id
            };
            _titreBusiness.IncrementLike(titre);
            return View(nameof(TitreController.Index), titreViewModel);
        }
        #endregion

        #region Commentaire
        [HttpPost]
        public IActionResult Commentaire(TitreViewModel titreViewModel)
        {
            Titre titre = _titreRepository.Find(titreViewModel.IdTitre);
            TitreViewModel newTitreViewModel = new TitreViewModel
            {
                Titre = titre,
                IdTitre = titre.IdTitre
            };
            if (ModelState.IsValid)
            {

                Commentaire commentaire = new Commentaire
                {
                    Auteur = titreViewModel.NewCommentaire.Auteur,
                    Contenu = titreViewModel.NewCommentaire.Contenu,
                    DateCreation = DateTime.Now,
                    IdTitre = newTitreViewModel.IdTitre,
                    Titre = _titreRepository.Find(newTitreViewModel.IdTitre)
                };
                _commentaireRepository.Add(commentaire);
                return RedirectToAction(nameof(TitreController.Index), "Titre", new { id = titreViewModel.IdTitre });

            }
            else
            {
                return this.View(nameof(TitreController.Index), newTitreViewModel);
            }
        }
        #endregion
    }
}