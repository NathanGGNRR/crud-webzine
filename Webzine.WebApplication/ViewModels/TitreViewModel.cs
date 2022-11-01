using Webzine.Entity;

namespace Webzine.WebApplication.ViewModels
{
    public class TitreViewModel
    {
        public Titre Titre { get; set; }

        public int IdTitre { get; set; }
        public Commentaire NewCommentaire { get; set; }
    }
}
