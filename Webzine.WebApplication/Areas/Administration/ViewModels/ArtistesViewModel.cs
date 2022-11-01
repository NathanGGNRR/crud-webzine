using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class ArtisteViewModel
    {
        [HiddenInput]
        public int IdArtiste { get; set; }
        [Required(ErrorMessage = "Le nom de l'artiste doit être renseigné !"), MinLength(1, ErrorMessage = "Le nom de l'artiste doit être supérieur à 0 caractère !"), MaxLength(50, ErrorMessage = "Le nom de l'artiste doit être inférieur à 51 caractères !"), StringLength(50, MinimumLength = 1, ErrorMessage = "Le nom de l'artiste doit être supérieur à 0 caractère !")]
        public string NomArtiste { get; set; }
        [Required(ErrorMessage = "La biographie de l'artiste doit être renseignée !")]
        public string Biographie { get; set; }

    }
}
