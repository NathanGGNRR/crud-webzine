using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class TitresController : Controller
    {

        #region Variables
        private ITitreRepository _titreRepository;
        private IStyleRepository _styleRepository;
        private IArtisteRepository _artisteRepository;
        private IConfiguration _configuration;

        private List<TitresViewModel> _titres = new List<TitresViewModel>();
        private TitresViewModel Titre { get; set; }
        #endregion

        public TitresController(ITitreRepository titreRepository, IStyleRepository styleRepository, IArtisteRepository artisteRepository, IConfiguration configuration)
        {
            _titreRepository = titreRepository;
            _styleRepository = styleRepository;
            _artisteRepository = artisteRepository;
            _configuration = configuration;
        }

        #region Index
        public IActionResult Index()
        {

            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            _titres = _titreRepository.Take(0, (_titreRepository.Count() <= lengthPage) ? _titreRepository.Count() : lengthPage).Select(vm => new TitresViewModel
            {
                IdTitre = vm.IdTitre,
                IdArtiste = vm.IdArtiste,
                NomTitre = vm.Libelle,
                NbLectures = vm.NbLectures,
                NbLikes = vm.NbLikes,
                NomArtiste = vm.Artiste.Nom,
                NbCommentaires = (vm.Commentaires != null) ? vm.Commentaires.Count : 0,
                Duree = vm.Duree,
                DateSortie = vm.DateSortie
            }).ToList();


            IndexTitresViewModel indextitresViewModel = new IndexTitresViewModel
            {
                Titres = _titres,
                TotalTitres = _titreRepository.Count(),
                PageActuel = 1,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_titreRepository.Count() - lengthPage > 0) ? true : false
            };

            return this.View(nameof(TitresController.Index), indextitresViewModel);

        }

        [HttpGet]
        public IActionResult Navigate(int id)
        {
            int lengthPage = Int32.Parse(_configuration["lengthAdminPage"]);
            int displayPage = Int32.Parse(_configuration["displayAdminPage"]);
            int indexActuel = (id - 1) * lengthPage;
            _titres = _titreRepository.Take(indexActuel, (_titreRepository.Count() <= (id * lengthPage)) ? _titreRepository.Count() - indexActuel : lengthPage).Select(vm =>
               new TitresViewModel
               {
                   IdTitre = vm.IdTitre,
                   IdArtiste = vm.IdArtiste,
                   NomTitre = vm.Libelle,
                   NbLectures = vm.NbLectures,
                   NbLikes = vm.NbLikes,
                   NomArtiste = vm.Artiste.Nom,
                   NbCommentaires = (vm.Commentaires != null) ? vm.Commentaires.Count : 0,
                   Duree = vm.Duree,
                   DateSortie = vm.DateSortie
               }).ToList();


            IndexTitresViewModel indextitresViewModel = new IndexTitresViewModel
            {
                Titres = _titres,
                TotalTitres = _titreRepository.Count(),
                PageActuel = id,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_titreRepository.Count() - indexActuel > lengthPage) ? true : false
            };

            return this.View(nameof(TitresController.Index), indextitresViewModel);
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
                    return this.View(nameof(TitresController.Manage), InitializeList(id));
                }
                catch (NullReferenceException e)
                {
                    return RedirectToAction(nameof(TitresController.Index), "Titres", new { area = "Administration" });
                }
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPost, ActionName(nameof(TitresController.Manage)), ValidateAntiForgeryToken]
        public IActionResult ManagePOST(ManageTitresViewModel manageTitresViewModel)
        {
            if (ModelState.IsValid)
            {

                //Get selected and affect them in new list of style
                List<Style> styles = new List<Style>();
                Titre titre = new Titre((manageTitresViewModel.Titre.IdTitre != 0) ? manageTitresViewModel.Titre.IdTitre : 0, manageTitresViewModel.Titre.IdArtiste, _artisteRepository.Find(manageTitresViewModel.Titre.IdArtiste), manageTitresViewModel.Titre.Chronique, manageTitresViewModel.Titre.Album, manageTitresViewModel.Titre.DateSortie, manageTitresViewModel.Titre.Duree, manageTitresViewModel.Titre.NomTitre, manageTitresViewModel.Titre.NbLectures, manageTitresViewModel.Titre.NbLikes, manageTitresViewModel.Titre.UrlEcoute, manageTitresViewModel.Titre.UrlJaquette);
                var liens = Request.Form["style.IdStyle"].Select(id => new LienStyle { IdTitre = (manageTitresViewModel.Titre.IdTitre != 0) ? manageTitresViewModel.Titre.IdTitre : 0, Titre = titre, IdStyle = Convert.ToInt32(id), Style = _styleRepository.Find(Convert.ToInt32(id)) }).ToList();
                titre.LienStyle = liens;

                if (manageTitresViewModel.Titre.IdTitre != 0) { _titreRepository.Update(titre); } else { _titreRepository.Add(titre); }
                return RedirectToAction(nameof(TitresController.Index), "Titres", new { area = "Administration" });
            }
            else
            {
                InitializeArtistesList(manageTitresViewModel);

                //Get already selected and affect them in new list of styleviewmodel to display them again
                manageTitresViewModel.Titre.Styles = Request.Form["style.IdStyle"].ToList().Select(vm =>
                {
                    Style style = _styleRepository.Find(Convert.ToInt32(vm));
                    return new StylesViewModel { IdStyle = style.IdStyle, Libelle = style.Libelle, Checked = true };
                }).ToList();

                InitializeStyleList(manageTitresViewModel);
                return this.View(nameof(TitresController.Manage), manageTitresViewModel);
            }
        }
        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Titre titre = _titreRepository.Find(id);
                TitresViewModel vm = new TitresViewModel
                {
                    IdTitre = titre.IdTitre,
                    NomTitre = titre.Libelle
                };

                return this.View(nameof(TitresController.Delete), vm);
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(TitresController.Index), "Titres", new { area = "Administration" });
            }
        }

        [HttpPost, ActionName(nameof(TitresController.Delete)), ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            try
            {
                _titreRepository.Delete(_titreRepository.Find(id));
                return RedirectToAction(nameof(ArtistesController.Index), "Titres", new { area = "Administration" });
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(TitresController.Index), "Titres", new { area = "Administration" });
            }
        }

        #endregion

        #region Méthodes
        /// <summary>
        /// Permet d'initliaze la liste d'artistes et de styles avec id du titre en paramètre si edit et null si create
        /// </summary>
        private ManageTitresViewModel InitializeList(int? id)
        {
            ManageTitresViewModel titresMTVM = new ManageTitresViewModel();
            titresMTVM.Titre = new TitresViewModel { IdTitre = (id.HasValue) ? (int)id : 0, DateSortie = DateTime.Now, Styles = new List<StylesViewModel>() };
            if (id.HasValue)
            { //Si la valeur id n'est pas égal à null, la propriété Titre est affecté.
                Titre titre = _titreRepository.Find((int)id);
                titresMTVM.Titre.Styles = titre.LienStyle.Select(x => new StylesViewModel { IdStyle = x.IdStyle }).ToList();
                titresMTVM.Titre.Album = titre.Album;
                titresMTVM.Titre.IdArtiste = titre.IdArtiste;
                titresMTVM.Titre.NomTitre = titre.Libelle;
                titresMTVM.Titre.NbLectures = titre.NbLectures;
                titresMTVM.Titre.NbLikes = titre.NbLikes;
                titresMTVM.Titre.NomArtiste = titre.Artiste.Nom;
                titresMTVM.Titre.Duree = titre.Duree;
                titresMTVM.Titre.DateSortie = titre.DateSortie;
                titresMTVM.Titre.Chronique = titre.Chronique;
                titresMTVM.Titre.UrlEcoute = titre.UrlEcoute;
                titresMTVM.Titre.UrlJaquette = titre.UrlJaquette;
            }
            InitializeArtistesList(titresMTVM);
            InitializeStyleList(titresMTVM);
            return titresMTVM;
        }


        /// <summary>
        /// Intialize la liste d'artiste 
        /// </summary>
        private void InitializeArtistesList(ManageTitresViewModel manage)
        {
            //Initialize un SelectList pour asp-for dans la vue. Le premier paramètre est la liste à convertir, le deuxième est la propriété que l'on va afficher dans la liste, le troisième est la valeur affecter à une ligne
            //Le dernier paramètre est égal à l'artiste du titre si c'est un edit sinon c'est égal à null
            manage.Artistes = new SelectList(_artisteRepository.FindAll().Select(vm => new ArtisteViewModel { IdArtiste = vm.IdArtiste, NomArtiste = vm.Nom }), nameof(ArtisteViewModel.IdArtiste), nameof(ArtisteViewModel.NomArtiste), _artisteRepository.Find(manage.Titre.IdArtiste));

        }

        /// <summary>
        /// Intialize la liste de style en cochant si edit
        /// </summary>
        private void InitializeStyleList(ManageTitresViewModel manage)
        {
            //Pour chaque style, si c'est edit on met la valeur à true si le titre est dans ce style sinon false
            manage.Styles = _styleRepository.FindAll().Select(vm => new StylesViewModel { IdStyle = vm.IdStyle, Libelle = vm.Libelle, Checked = (manage.Titre.Styles.Any(x => x.IdStyle == vm.IdStyle)) ? true : false }).ToList();
        }
        #endregion


    }
}