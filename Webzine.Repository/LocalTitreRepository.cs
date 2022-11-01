using System;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class LocalTitreRepository : ITitreRepository
    {
        /// <summary>
        /// Ajoute un Titre dans le set de données
        /// </summary>
        /// <param name="titre">Titre à ajouter</param>
        void ITitreRepository.Add(Titre titre)
        {
            titre.IdTitre = StaticFactory.Titres.Max(x => x.IdTitre) + 1;

            // Ajout du titre dans le set de données
            StaticFactory.Titres.Add(titre);

            titre.LienStyle.ToList().ForEach(ls =>
            {
                ls.IdTitre = titre.IdTitre;
                StaticFactory.LienStyles.Add(ls);
            });

            StaticFactory.UpdateBinavigabilite();
        }

        /// <summary>
        /// Retourne le nombre de titres présent dans le set de données
        /// </summary>
        /// <returns>Nombre de titres présents dans le set de données</returns>
        int ITitreRepository.Count()
        {
            return StaticFactory.Titres.Count;
        }

        /// <summary>
        /// Supprime un titre du set de données
        /// </summary>
        /// <param name="titre">Titre à supprimer</param>
        void ITitreRepository.Delete(Titre titre)
        {
            // Si le titre existe dans le set, suppression
            if (StaticFactory.Titres.Contains(titre))
            {
                StaticFactory.LienStyles.Where(x => x.IdTitre == titre.IdTitre).ToList().ForEach(ls => StaticFactory.LienStyles.Remove(ls));

                StaticFactory.Commentaires.Where(x => x.IdTitre == titre.IdTitre).ToList().ForEach(x => StaticFactory.Commentaires.Remove(x));
                StaticFactory.Titres.Remove(titre);
                StaticFactory.UpdateBinavigabilite();
            }
        }

        /// <summary>
        /// Retourne un titre du set de données
        /// </summary>
        /// <param name="idtitre">Identifiant du titre recherché</param>
        /// <returns>Titre correspondant</returns>
        Titre ITitreRepository.Find(int idTitre)
        {
            return StaticFactory.Titres.FirstOrDefault(x => x.IdTitre == idTitre);
        }

        /// <summary>
        /// Retourne les titres demandés (pour la pagination) triés selon la date de création (du plus récent au plus ancien)
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns>Titres correspondant</returns>
        IEnumerable<Titre> ITitreRepository.FindTitres(int offset, int limit)
        {
            return StaticFactory.Titres.OrderByDescending(t => t.DateSortie).Skip(offset).Take(limit);
        }

        /// <summary>
        /// Retourne tous les titres
        /// </summary>
        /// <returns>Tous les titres contenus dans le set de données</returns>
        IEnumerable<Titre> ITitreRepository.FindAll()
        {
            return StaticFactory.Titres;
        }

        /// <summary>
        /// Incrémente le nombre de lectures d'un titre
        /// </summary>
        /// <param name="titre">Titre à modifier</param>
        void ITitreRepository.IncrementNbLectures(Titre titre)
        {
            StaticFactory.Titres.FirstOrDefault(t => t == titre).NbLectures++;
        }

        /// <summary>
        /// Incrémente le nombre de likes d'un titre
        /// </summary>
        /// <param name="titre">Titre à modifier</param>
        void ITitreRepository.IncrementNbLikes(Titre titre)
        {
            StaticFactory.Titres.FirstOrDefault(t => t == titre).NbLikes++;
        }

        /// <summary>
        /// Recherche les titres concernant le mot recherché
        /// </summary>
        /// <param name="mot">Mots clés</param>
        /// <returns>Titres correspondants</returns>
        IEnumerable<Titre> ITitreRepository.Search(string mot)
        {
            return StaticFactory.Titres.Where(t => t.Libelle.Contains(mot, System.StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Recherche les titres en fonction des styles
        /// </summary>
        /// <param name="mot">Mots clés</param>
        /// <returns>Titres correspondants</returns>
        IEnumerable<Titre> ITitreRepository.SearchByStyle(string mot)
        {
            return StaticFactory.LienStyles.Select(x => x.Titre);
        }

        /// <summary>
        /// Met à jour un titre
        /// </summary>
        /// <param name="titre">Titre mis à jour</param>
        void ITitreRepository.Update(Titre titre)
        {
            // Si l'artiste existe bien
            if (!StaticFactory.Titres.FirstOrDefault(a => a.IdTitre == titre.IdTitre).Equals(default(Titre)))
            {
                Titre titreStatic = StaticFactory.Titres.FirstOrDefault(a => a.IdTitre == titre.IdTitre);
                titreStatic.IdArtiste = titre.IdArtiste;
                titreStatic.Artiste = titre.Artiste;
                titreStatic.Libelle = titre.Libelle;
                titreStatic.Album = titre.Album;
                titreStatic.LienStyle = titre.LienStyle;
                titreStatic.UrlEcoute = titre.UrlEcoute;
                titreStatic.UrlJaquette = titre.UrlJaquette;
                titreStatic.Chronique = titre.Chronique;
                titreStatic.DateSortie = titre.DateSortie;
                titreStatic.Duree = titre.Duree;
                StaticFactory.LienStyles.RemoveAll(x => x.IdTitre == titre.IdTitre);
                StaticFactory.LienStyles.AddRange(titre.LienStyle);
            }
        }

        /// <summary>
        /// Obtenir les x titres les plus populaires
        /// </summary>
        /// <param name="date">Date limite de la recherche</param>
        /// <param name="limit">limite de resultats</param>
        /// <returns>Titres les plus populaires</returns>
        IEnumerable<Titre> ITitreRepository.GetPopular(DateTime date, int limit)
        {
            return StaticFactory.Titres.OrderByDescending(t => t.NbLikes).Where(r => r.DateCreation > date).Take(limit).ToList();
        }


        /// <summary>
        /// Retourne une fourchette de titres
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Titre> ITitreRepository.Take(int index, int length)
        {
            return StaticFactory.Titres.OrderByDescending(x => x.DateSortie).ToList().GetRange(index, length);
        }

        /// <summary>
        /// Permet de sauvegarder les changements dans la base de données
        /// </summary>
        void ITitreRepository.Save() { }
    }
}
