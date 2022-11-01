using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webzine.Entity
{
    /// <summary>
    /// Contient les différentes informations concernant un commentaire.
    /// </summary>
    [Table("Commentaires")]
    public class Commentaire
    {
        [Display(Name = "IdCommentaire"), Key]
        public int IdCommentaire { get; set; }

        [Display(Name = "Nom"), MinLength(2, ErrorMessage = "Le nom de l'auteur doit être supérieur à 1 caractère !"), MaxLength(30, ErrorMessage = "Le nom de l'auteur doit être inférieur à 31 caractères !"), Required(ErrorMessage = "Le nom de l'auteur doit être renseigné !"), StringLength(30, MinimumLength = 2, ErrorMessage = "La chronique doit être supérieur à 1 caractère !")]
        public string Auteur { get; set; }

        [Display(Name = "Commentaire"), MinLength(10, ErrorMessage = "Le contenu du commentaire doit être supérieur à 9 caractères !"), MaxLength(1000, ErrorMessage = "Le contenu du commentaire doit être inférieur à 1001 caractères !"), Required(ErrorMessage = "Le contenu du commentaire doit être renseigné !"), StringLength(1000, MinimumLength = 10, ErrorMessage = "La chronique doit être supérieur à 10 caractères !")]
        public string Contenu { get; set; }

        [Display(Name = "Date de création"), Required]
        public DateTime DateCreation { get; set; }

        [Display(Name = "IdTitre")]
        public int IdTitre { get; set; }

        [Display(Name = "Titre"), ForeignKey("IdTitre")]
        public Titre Titre { get; set; }
    }
}
