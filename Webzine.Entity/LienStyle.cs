using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webzine.Entity
{
    [Table("LiensStyle")]
    public class LienStyle
    {
        [Display(Name = "IdStyle")]
        public int IdStyle { get; set; }

        [Display(Name = "IdTitre")]
        public int IdTitre { get; set; }

        public Style Style { get; set; }
        public Titre Titre { get; set; }
    }
}
