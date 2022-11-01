namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TitresViewModel
    {
        public string NomArtiste { get; set; }
        [Required(ErrorMessage = "Le nom du titre doit être renseigné !"), MinLength(1, ErrorMessage = "Le nom du titre doit être supérieur à 0 caractère !"), MaxLength(200, ErrorMessage = "Le nom du titre doit être inférieur à 201 caractères !"), StringLength(200, MinimumLength = 1, ErrorMessage = "Le nom du titre doit être supérieur à 1 caractère !")]
        public string NomTitre { get; set; }
        [Required(ErrorMessage = "Le nom de l'album doit être renseigné !")]
        public string Album { get; set; }
        [HiddenInput]
        public int IdTitre { get; set; }
        public int IdArtiste { get; set; }
        public int Duree { get; set; }
        [Required(ErrorMessage = "L'url de la jacquette doit être renseigné !"), MaxLength(250, ErrorMessage = "L'url de la jacquette  doit être inférieur à 251 caractères !")]
        public string UrlJaquette { get; set; }
        [MinLength(13, ErrorMessage = "L'url de l'écoute doit être supérieur à 12 caractères !"), MaxLength(250, ErrorMessage = "L'url de l'écoute doit être inférieur à 251 caractères !"), StringLength(250, MinimumLength = 13, ErrorMessage = "L'url de l'écoute doit être supérieur à 12 caractères !")]
        public string UrlEcoute { get; set; }
        [DataType(DataType.Date), Required(ErrorMessage = "La date de sortie doit être renseignée !")]
        public DateTime DateSortie { get; set; }
        public int NbLectures { get; set; }
        public int NbLikes { get; set; }
        public int NbCommentaires { get; set; }
        public List<StylesViewModel> Styles { get; set; }
        [Required(ErrorMessage = "La chronique doit être renseignée !"), MinLength(10, ErrorMessage = "La chronique doit être supérieur à 9 caractères !"), MaxLength(4000, ErrorMessage = "La chronique doit être inférieur à 4001 caractères !"), StringLength(4000, MinimumLength = 10, ErrorMessage = "La chronique doit être supérieur à 12 caractères !")]
        public string Chronique { get; set; }

    }
}
