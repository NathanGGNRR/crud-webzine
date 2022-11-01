using System;
using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.WebApplication.ViewModels
{
    /// <summary>
    /// Données passées à la view Home (page d'accueil du site)
    /// </summary>
    public class HomeViewModel
    {
        public List<Titre> DerniersTitres { get; set; }
        public List<Titre> TitresPopulaires { get; set; }
        public int PageActuel { get; set; }
        public Boolean Next { get; set; }
    }
}
