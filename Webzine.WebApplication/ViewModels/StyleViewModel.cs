using System;
using Webzine.Entity;

namespace Webzine.WebApplication.ViewModels
{
    /// <summary>
    /// Données passées à la view Home (page d'accueil du site)
    /// </summary>
    public class StyleViewModel
    {
        public Style Style { get; set; }
        public int PageActuel { get; set; }
        public Boolean Next { get; set; }
    }
}
