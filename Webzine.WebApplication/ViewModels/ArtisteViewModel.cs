using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.WebApplication.ViewModels
{
    public class ArtisteViewModel
    {
        public Artiste Artist { get; set; }
        public List<List<Titre>> Albums { get; set; }
    }
}
