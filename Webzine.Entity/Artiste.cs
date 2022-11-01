using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webzine.Entity
{
    /// <summary>
    /// Contient les différentes information concernant un artiste, ainsi que les titres associés à celui ci.
    /// </summary>
    [Table("Artistes")]
    public class Artiste
    {
        [Display(Name = "IdArtiste"), Key]
        public int IdArtiste { get; set; }

        [Display(Name = "Nom de l'artiste"), MinLength(1, ErrorMessage = "Le nom de l'artiste doit être supérieur à 0 caractère !"), MaxLength(50, ErrorMessage = "Le nom de l'artiste doit être inférieur à 51 caractères !"), Required]
        public string Nom { get; set; }

        [Display(Name = "Biographie"), Required]
        public string Biographie { get; set; }

        [Display(Name = "Titres"), InverseProperty("Artiste")]
        public List<Titre> Titres { get; set; }
    }
}
