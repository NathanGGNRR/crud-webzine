using System.ComponentModel.DataAnnotations;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class StylesViewModel
    {
        public int IdStyle { get; set; }
        [Required(ErrorMessage = "Le nom du style doit être renseigné !"), MinLength(2, ErrorMessage = "Le nom du style doit être supérieur à 1 caractère !"), MaxLength(50, ErrorMessage = "Le nom du style doit être inférieur à 51 caractères !"), StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom du style doit être supérieur à 1 caractère !")]
        public string Libelle { get; set; }
        public bool Checked { get; set; }
    }
}
