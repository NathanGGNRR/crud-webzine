using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Webzine.EntitiesContext;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class DbTitreRepository : ITitreRepository
    {
        private WebzineDbContext _dbContext = new WebzineDbContext();


        /// <summary>
        /// Ajoute un Titre dans le set de données
        /// </summary>
        /// <param name="titre">Titre à ajouter</param>
        void ITitreRepository.Add(Titre titre)
        {

            Artiste artisteContext = _dbContext.Artistes.FirstOrDefault(x => x.IdArtiste == titre.IdArtiste);
            titre.IdArtiste = artisteContext.IdArtiste;
            titre.Artiste = artisteContext;

            List<LienStyle> liensStyle = new List<LienStyle>();
            foreach (var newStyle in titre.LienStyle)
            {
                Style style = _dbContext.Styles.FirstOrDefault(styleContext => styleContext.IdStyle == newStyle.IdStyle);
                liensStyle.Add(new LienStyle { IdStyle = style.IdStyle, Style = style, IdTitre = titre.IdTitre, Titre = titre }); ;
            }
            titre.LienStyle.Clear();
            titre.LienStyle = liensStyle;

            _dbContext.Titres.Add(titre);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Retourne le nombre de titres présent dans le set de données
        /// </summary>
        /// <returns>Nombre de titres présents dans le set de données</returns>
        int ITitreRepository.Count()
        {
            return _dbContext.Titres.Count();
        }

        /// <summary>
        /// Supprime un titre du set de données
        /// </summary>
        /// <param name="titre">Titre à supprimer</param>
        void ITitreRepository.Delete(Titre titre)
        {
            _dbContext.Titres.Remove(titre);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Retourne un titre du set de données
        /// </summary>
        /// <param name="idtitre">Identifiant du titre recherché</param>
        /// <returns>Titre correspondant</returns>
        Titre ITitreRepository.Find(int idTitre)
        {
            return _dbContext.Titres.Where(t => t.IdTitre == idTitre).Include(t => t.Artiste).Include(t => t.Commentaires).Include(t => t.LienStyle).ThenInclude(l => l.Style).FirstOrDefault();
        }

        /// <summary>
        /// Retourne les titres demandés (pour la pagination) triés selon la date de création (du plus récent au plus ancien)
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IEnumerable<Titre> ITitreRepository.FindTitres(int offset, int limit)
        {
            return _dbContext.Titres.OrderByDescending(t => t.DateSortie).Skip(offset).Take(limit).Include(t => t.Artiste).Include(t => t.LienStyle).ThenInclude(l => l.Style).ToList();
        }

        /// <summary>
        /// Retourne tous les titres
        /// </summary>
        /// <returns>Tous les titres contenus dans le set de données</returns>
        IEnumerable<Titre> ITitreRepository.FindAll()
        {
            return _dbContext.Titres.Include(t => t.Artiste).Include(t => t.LienStyle).ThenInclude(ls => ls.Style).ToList();
        }

        /// <summary>
        /// Incrémente le nombre de lectures d'un titre
        /// </summary>
        /// <param name="titre">Titre à modifier</param>
        void ITitreRepository.IncrementNbLectures(Titre titre)
        {
            Titre titreContext = _dbContext.Titres.FirstOrDefault(t => t.IdTitre == titre.IdTitre);
            titreContext.NbLectures = titre.NbLectures;
        }

        /// <summary>
        /// Incrémente le nombre de likes d'un titre
        /// </summary>
        /// <param name="titre">Titre à modifier</param>
        void ITitreRepository.IncrementNbLikes(Titre titre)
        {
            Titre titreContext = _dbContext.Titres.FirstOrDefault(t => t.IdTitre == titre.IdTitre);
            titreContext.NbLikes = titre.NbLikes;
        }

        /// <summary>
        /// Recherche les titres concernant le mot recherché
        /// </summary>
        /// <param name="mot">Mots clés</param>
        /// <returns>Titres correspondants</returns>
        IEnumerable<Titre> ITitreRepository.Search(string mot)
        {
            return _dbContext.Titres.Where(t => t.Libelle.ToLower().Contains(mot.ToLower())).Include(t => t.Artiste).Include(t => t.LienStyle).ThenInclude(l => l.Style).ToList();
        }

        /// <summary>
        /// Recherche les titres en fonction des styles
        /// </summary>
        /// <param name="mot">Mots clés</param>
        /// <returns>Styles correspondants</returns>
        IEnumerable<Titre> ITitreRepository.SearchByStyle(string mot)
        {
            return _dbContext.Titres.Where(t => t.LienStyle.Select(s => s.Style.Libelle).ToString() == mot).Include(t => t.Artiste).Include(t => t.LienStyle).ThenInclude(l => l.Style).ToList();
        }

        /// <summary>
        /// Met à jour un titre
        /// </summary>
        /// <param name="titre">Titre mis à jour</param>
        void ITitreRepository.Update(Titre titre)
        {
            Titre titreContext = _dbContext.Titres.Where(t => t.IdTitre == titre.IdTitre).Include(t => t.Artiste).Include(t => t.Commentaires).Include(t => t.LienStyle).ThenInclude(l => l.Style).FirstOrDefault();
            Artiste artisteContext = _dbContext.Artistes.FirstOrDefault(x => x.IdArtiste == titre.IdArtiste);
            titreContext.IdTitre = titre.IdTitre;
            titreContext.Libelle = titre.Libelle;
            titreContext.IdArtiste = artisteContext.IdArtiste;
            titreContext.Artiste = artisteContext;
            titreContext.Chronique = titre.Chronique;
            titreContext.DateSortie = titre.DateSortie;
            titreContext.Duree = titre.Duree;
            titreContext.UrlEcoute = titre.UrlEcoute;
            titreContext.UrlJaquette = titre.UrlJaquette;
            titreContext.LienStyle.ToList().ForEach(s =>
            {
                if (!titre.LienStyle.Any(x => x.IdStyle == s.IdStyle))
                {
                    titreContext.LienStyle.Remove(s);
                }
            });

            foreach (var newStyle in titre.LienStyle)
            {
                if (!titreContext.LienStyle.Any(x => x.IdStyle == newStyle.IdStyle))
                {

                    Style style = _dbContext.Styles.FirstOrDefault(styleContext => styleContext.IdStyle == newStyle.IdStyle);
                    titreContext.LienStyle.Add(new LienStyle { IdStyle = style.IdStyle, Style = style, IdTitre = titre.IdTitre, Titre = titreContext }); ;
                }
            }

            _dbContext.SaveChanges();
        }


        /// <summary>
        /// Obtenir les x titre les plus populaire
        /// </summary>
        /// <param name="date">Date limit de la recherche</param>
        /// /// <param name="limit">limite de resulta</param>
        /// <returns></returns>
        public IEnumerable<Titre> GetPopular(DateTime date, int limit)
        {
            return _dbContext.Titres.AsNoTracking().OrderByDescending(t => t.NbLikes).Where(r => r.DateCreation > date).Take(limit).Include(t => t.Artiste).Include(t => t.LienStyle).ThenInclude(l => l.Style).ToList();
        }


        /// <summary>
        /// Retourne une fourchette de titres
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Titre> ITitreRepository.Take(int index, int length)
        {
            return _dbContext.Titres.AsNoTracking().OrderByDescending(x => x.DateSortie).Skip(index).Take(length).Include(t => t.Artiste).Include(t => t.Commentaires).Include(t => t.LienStyle).ThenInclude(l => l.Style).ToList();
        }

        /// <summary>
        /// Permet de sauvegarder les changements dans la base de données
        /// </summary>
        void ITitreRepository.Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
