using System;
using System.ComponentModel.DataAnnotations;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class CommentairesViewModel
    {
        public int IdCommentaire { get; set; }
        public int IdTitre { get; set; }
        [Required]
        public string Auteur { get; set; }
        [Required]
        public string Contenu { get; set; }

        public DateTime DateCreation { get; set; }

        public string NomTitre { get; set; }

        public string NomArtiste { get; set; }
    }
}
