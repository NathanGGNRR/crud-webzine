namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    [Area("Administration")]
    public class CommentairesController : Controller
    {

        #region Variables
        private ICommentaireRepository _commentaireRepository;
        private IConfiguration _configuration;
        private List<CommentairesViewModel> _commentaires = new List<CommentairesViewModel>();
        #endregion


        public CommentairesController(ICommentaireRepository commentaireRepository, IConfiguration configuration)
        {
            _commentaireRepository = commentaireRepository;
            _configuration = configuration;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            _commentaires = _commentaireRepository.Take(0, (_commentaireRepository.Count() <= lengthPage) ? _commentaireRepository.Count() : lengthPage).Select(vm => new CommentairesViewModel
            {
                Auteur = vm.Auteur,
                Contenu = vm.Contenu,
                DateCreation = vm.DateCreation,
                IdCommentaire = vm.IdCommentaire,
                IdTitre = vm.IdTitre,
                NomTitre = vm.Titre.Libelle,
                NomArtiste = vm.Titre.Artiste.Nom,
            }).ToList();


            IndexCommentairesViewModel indexCommentairesViewModel = new IndexCommentairesViewModel
            {
                Commentaires = _commentaires,
                TotalCommentaires = _commentaireRepository.Count(),
                PageActuel = 1,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_commentaireRepository.Count() - lengthPage > 0) ? true : false
            };

            return this.View(nameof(CommentairesController.Index), indexCommentairesViewModel);

        }

        [HttpGet]
        public IActionResult Navigate(int id)
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            int indexActuel = (id - 1) * lengthPage;
            _commentaires = _commentaireRepository.Take(indexActuel, (_commentaireRepository.Count() <= (id * lengthPage)) ? _commentaireRepository.Count() - indexActuel : lengthPage).Select(vm =>
                new CommentairesViewModel
                {
                    Auteur = vm.Auteur,
                    Contenu = vm.Contenu,
                    DateCreation = vm.DateCreation,
                    IdCommentaire = vm.IdCommentaire,
                    IdTitre = vm.IdTitre,
                    NomTitre = vm.Titre.Libelle,
                    NomArtiste = vm.Titre.Artiste.Nom,
                }
                ).ToList();


            IndexCommentairesViewModel indexCommentairesViewModel = new IndexCommentairesViewModel
            {
                Commentaires = _commentaires,
                TotalCommentaires = _commentaireRepository.Count(),
                PageActuel = id,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_commentaireRepository.Count() - indexActuel > lengthPage) ? true : false
            };

            return this.View(nameof(CommentairesController.Index), indexCommentairesViewModel);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Commentaire commentaire = _commentaireRepository.Find(id);
                CommentairesViewModel vm = new CommentairesViewModel
                {
                    Auteur = commentaire.Auteur,
                    DateCreation = commentaire.DateCreation,
                    IdCommentaire = commentaire.IdCommentaire,
                    NomTitre = commentaire.Titre.Libelle,
                    NomArtiste = commentaire.Titre.Artiste.Nom
                };

                return this.View(nameof(CommentairesController.Delete), vm);
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(CommentairesController.Index), "Commentaires", new { area = "Administration" });
            }
        }

        [HttpPost, ActionName(nameof(CommentairesController.Delete)), ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            try
            {
                _commentaireRepository.Delete(_commentaireRepository.Find(id));
                return RedirectToAction(nameof(ArtistesController.Index), "Commentaires", new { area = "Administration" });
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(CommentairesController.Index), "Commentaires", new { area = "Administration" });
            }
        }
        #endregion

    }
}
