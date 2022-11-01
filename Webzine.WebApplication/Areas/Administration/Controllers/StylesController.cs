
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
    public class StylesController : Controller
    {


        #region Variables
        private IStyleRepository _styleRepository;
        private IConfiguration _configuration;
        private List<StylesViewModel> _styles = new List<StylesViewModel>();
        private StylesViewModel Style { get; set; }
        #endregion

        public StylesController(IStyleRepository styleRepository, IConfiguration configuration)
        {
            _styleRepository = styleRepository;
            _configuration = configuration;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            _styles = _styleRepository.Take(0, (_styleRepository.Count() <= lengthPage) ? _styleRepository.Count() : lengthPage).Select(vm => new StylesViewModel { IdStyle = vm.IdStyle, Libelle = vm.Libelle }).ToList();

            IndexStylesViewModel indexStylesViewModel = new IndexStylesViewModel
            {
                Styles = _styles,
                TotalStyles = _styleRepository.Count(),
                PageActuel = 1,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_styleRepository.Count() - lengthPage > 0) ? true : false
            };

            return this.View(nameof(StylesController.Index), indexStylesViewModel);

        }

        [HttpGet]
        public IActionResult Navigate(int id)
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            int indexActuel = (id - 1) * lengthPage;
            _styles = _styleRepository.Take(indexActuel, (_styleRepository.Count() <= (id * lengthPage)) ? _styleRepository.Count() - indexActuel : lengthPage).Select(vm =>
                new StylesViewModel { IdStyle = vm.IdStyle, Libelle = vm.Libelle }
            ).ToList();
            IndexStylesViewModel indexStylesViewModel = new IndexStylesViewModel
            {
                Styles = _styles,
                TotalStyles = _styleRepository.Count(),
                PageActuel = id,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_styleRepository.Count() - indexActuel > lengthPage) ? true : false
            };

            return this.View(nameof(StylesController.Index), indexStylesViewModel);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Style style = _styleRepository.Find(id);
                StylesViewModel vm = new StylesViewModel
                {
                    IdStyle = style.IdStyle,
                    Libelle = style.Libelle
                };
                return this.View(nameof(StylesController.Delete), vm);
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(StylesController.Index), "Styles", new { area = "Administration" });
            }
        }

        [HttpPost, ActionName(nameof(StylesController.Delete))]
        public IActionResult DeletePOST(int id)
        {
            try
            {
                _styleRepository.Delete(_styleRepository.Find(id));
                return RedirectToAction(nameof(StylesController.Index), "Styles", new { area = "Administration" });
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(StylesController.Index), "Styles", new { area = "Administration" });
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
                    Style = new StylesViewModel();
                    if (id.HasValue)
                    {
                        Style style = _styleRepository?.Find((int)id);
                        Style = new StylesViewModel
                        {
                            IdStyle = style.IdStyle,
                            Libelle = style.Libelle,
                        };
                    }

                    return this.View(nameof(StylesController.Manage), Style);
                }
                catch (NullReferenceException e)
                {
                    return RedirectToAction(nameof(StylesController.Index), "Styles", new { area = "Administration" });
                }
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPost, ActionName(nameof(StylesController.Manage)), ValidateAntiForgeryToken]
        public IActionResult ManagePOST(StylesViewModel stylesViewModel)
        {
            if (ModelState.IsValid)
            {
                Style style = new Style
                {
                    IdStyle = (stylesViewModel.IdStyle != 0) ? stylesViewModel.IdStyle : 0,
                    Libelle = stylesViewModel.Libelle
                };

                if (stylesViewModel.IdStyle != 0) { _styleRepository.Update(style); } else { _styleRepository.Add(style); }

                return RedirectToAction(nameof(StylesController.Index), "Styles", new { area = "Administration" });
            }
            else
            {
                return this.View(nameof(StylesController.Manage), stylesViewModel);
            }
        }

        #endregion

    }
}
