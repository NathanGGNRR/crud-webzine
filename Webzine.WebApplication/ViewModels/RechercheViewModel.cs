using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.WebApplication.ViewModels
{
    /// <summary>
    /// Données passées à la view Home (page d'accueil du site)
    /// </summary>
    public class RechercheViewModel
    {
        public string Keywords { get; set; }
        public List<Artiste> Artistes { get; set; }
        public List<Titre> Titres { get; set; }

    }
}
