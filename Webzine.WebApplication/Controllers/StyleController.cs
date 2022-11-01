using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.ViewModels;

namespace Webzine.WebApplication.Controllers
{
    public class StyleController : Controller
    {

        #region Variables
        private IStyleRepository _styleRepository;
        private IConfiguration _configuration;
        #endregion

        public StyleController(IStyleRepository styleRepository, IConfiguration configuration)
        {
            _styleRepository = styleRepository;
            _configuration = configuration;
        }

        #region Index
        [HttpGet]
        public IActionResult Index(string name)
        {
            int lengthStylePage = Convert.ToInt32(_configuration["lengthStylePage"]);
            Style style = _styleRepository.FindByName(name);
            StyleViewModel styleViewModel = new StyleViewModel { Style = style, PageActuel = 0, Next = (style.LienStyle.Count - lengthStylePage > 0) ? true : false };
            styleViewModel.Style.LienStyle = style.LienStyle.ToList().GetRange(0, (styleViewModel.Style.LienStyle.Count - lengthStylePage > 0) ? lengthStylePage : style.LienStyle.Count);
            return this.View(nameof(StyleController.Index), styleViewModel);
        }

        [HttpGet]
        public IActionResult Navigate(string name, int id)
        {
            int lengthStylePage = Convert.ToInt32(_configuration["lengthStylePage"]);
            Style style = _styleRepository.FindByName(name);

            StyleViewModel styleVM = new StyleViewModel { Style = style, PageActuel = id, Next = true };
            if (id * lengthStylePage + lengthStylePage < style.LienStyle.Count)
            {
                styleVM.Style.LienStyle = styleVM.Style.LienStyle.Skip(id * lengthStylePage).Take(lengthStylePage).ToList();
            }
            else
            {
                styleVM.Style.LienStyle = styleVM.Style.LienStyle.ToList().GetRange(id * lengthStylePage, styleVM.Style.LienStyle.Count - (id * lengthStylePage));
                styleVM.Next = false;
            }

            return View(nameof(StyleController.Index), styleVM);
        }

        #endregion
    }
}
