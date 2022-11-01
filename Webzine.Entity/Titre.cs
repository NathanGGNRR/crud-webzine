using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webzine.Entity
{
    /// <summary>
    /// Contient les différentes information concernant un titre, ainsi que tout les éléments rattachés (commentaires...)
    /// </summary>
    [Table("Titres")]
    public class Titre
    {
        [Display(Name = "IdTitre"), Key]
        public int IdTitre { get; set; }

        [Display(Name = "IdArtiste")]
        public int IdArtiste { get; set; }

        [Display(Name = "Artiste"), ForeignKey("IdArtiste")]
        public Artiste Artiste { get; set; }

        [Display(Name = "Durée en secondes")]
        public int Duree { get; set; }

        [Display(Name = "Titre"), MinLength(1), MaxLength(200), Required]
        public string Libelle { get; set; }

        [Display(Name = "Chronique"), MinLength(10), MaxLength(4000), Required]
        public string Chronique { get; set; }

        [Display(Name = "Jaquette de l'album"), MaxLength(250), Required]
        public string UrlJaquette { get; set; }

        [Display(Name = "URL d'écoute"), MinLength(13), MaxLength(250)]
        public string UrlEcoute { get; set; }

        [Display(Name = "Date de création"), Required, DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }

        [Display(Name = "Date de sortie"), Required, DataType(DataType.Date)]
        public DateTime DateSortie { get; set; }

        [Display(Name = "Nombre de lectures"), Required]
        public int NbLectures { get; set; }

        [Display(Name = "Nombre de likes"), Required]
        public int NbLikes { get; set; }

        [Display(Name = "Album"), Required]
        public string Album { get; set; }

        [Display(Name = "Commentaires"), InverseProperty("Titre")]
        public List<Commentaire> Commentaires { get; set; }

        public ICollection<LienStyle> LienStyle { get; set; }


        public Titre()
        {
            LienStyle = new List<LienStyle>();
            Commentaires = new List<Commentaire>();
        }

        public Titre(int idTitre, int idArtiste, Artiste artiste, string chronique, DateTime dateSortie, int duree, string libelle, int nbLectures, int nbLikes, string urlEcoute, string urlJacquette, List<LienStyle> lienStyles)
        {
            IdTitre = idTitre;
            IdArtiste = idArtiste;
            Artiste = artiste;
            Chronique = chronique;
            DateSortie = dateSortie;
            Duree = duree;
            Libelle = libelle;
            NbLectures = nbLectures;
            NbLikes = nbLikes;
            UrlEcoute = urlEcoute;
            UrlJaquette = urlJacquette;
            Commentaires = new List<Commentaire>();
            LienStyle = lienStyles;
            DateCreation = DateTime.Now;
        }

        public Titre(int idTitre, int idArtiste, Artiste artiste, string chronique, string album, DateTime dateSortie, int duree, string libelle, int nbLectures, int nbLikes, string urlEcoute, string urlJacquette)
        {
            IdTitre = idTitre;
            IdArtiste = idArtiste;
            Artiste = artiste;
            Chronique = chronique;
            Album = album;
            DateSortie = dateSortie;
            Duree = duree;
            Libelle = libelle;
            NbLectures = nbLectures;
            NbLikes = nbLikes;
            UrlEcoute = urlEcoute;
            UrlJaquette = urlJacquette;
            Commentaires = new List<Commentaire>();
            DateCreation = DateTime.Now;
        }
    }
}
