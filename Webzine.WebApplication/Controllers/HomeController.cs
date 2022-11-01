using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Webzine.EntitiesContext;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.ViewModels;

namespace Webzine.WebApplication.Controllers
{
    public class HomeController : Controller
    {

        #region Variables
        private readonly WebzineDbContext _dbContext;
        private ITitreRepository _titreRepository;
        private IConfiguration _configuration;
        #endregion

        public HomeController(ITitreRepository titreRepository, IConfiguration configuration)
        {
            _dbContext = new WebzineDbContext();
            _titreRepository = titreRepository;
            _configuration = configuration;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthPage"]);
            int lengthPop = Convert.ToInt32(_configuration["lengthPop"]);
            var homeVM = new HomeViewModel
            {
                TitresPopulaires = _titreRepository.FindAll().OrderByDescending(x => x.NbLikes).Take(lengthPop).ToList(),
                DerniersTitres = _titreRepository.FindTitres(0, lengthPage).ToList(),
                PageActuel = 0,
                Next = (_titreRepository.Count() - lengthPage > 0) ? true : false
            };

            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Navigate(int id)
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthPage"]);
            int lengthPop = Convert.ToInt32(_configuration["lengthPop"]);
            var titres = new HomeViewModel();
            if (id * lengthPage + lengthPage < _titreRepository.Count())
            {
                titres.TitresPopulaires = _titreRepository.FindAll().OrderByDescending(x => x.NbLikes).Take(lengthPop).ToList();
                titres.DerniersTitres = _titreRepository.FindTitres(id * lengthPage, lengthPage).ToList();
                titres.PageActuel = id;
                titres.Next = true;
            }
            else
            {
                titres.TitresPopulaires = _titreRepository.FindAll().OrderByDescending(x => x.NbLikes).Take(lengthPop).ToList();
                titres.DerniersTitres = _titreRepository.FindTitres(id * lengthPage, _titreRepository.Count() - id * lengthPage).ToList();
                titres.PageActuel = id;
                titres.Next = false;
            }
            return View(nameof(HomeController.Index), titres);
        }

        #endregion
    }
}
