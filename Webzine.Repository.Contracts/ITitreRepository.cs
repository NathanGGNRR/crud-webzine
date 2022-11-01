using System;
using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.Repository.Contracts
{
    /// <summary>
    /// Permet d'accéder aux données des titres.
    /// </summary>
    public interface ITitreRepository
    {
        /// <summary>
        /// Ajoute un Titre dans le set de données
        /// </summary>
        /// <param name="titre">Titre à ajouter</param>
        public void Add(Titre titre);

        /// <summary>
        /// Retourne le nombre de titres présent dans le set de données
        /// </summary>
        /// <returns>Nombre de titres présents dans le set de données</returns>
        public int Count();

        /// <summary>
        /// Supprime un titre du set de données
        /// </summary>
        /// <param name="titre">Titre à supprimer</param>
        public void Delete(Titre titre);


        /// <summary>
        /// Retourne un titre du set de données
        /// </summary>
        /// <param name="idtitre">Identifiant du titre recherché</param>
        /// <returns>Titre correspondant</returns>
        public Titre Find(int idTitre);

        /// <summary>
        /// Retourne les titres demandés (pour la pagination) triés selon la date de création (du plus récent au plus ancien)
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns>Titres correspondant</returns>
        public IEnumerable<Titre> FindTitres(int offset, int limit);

        /// <summary>
        /// Retourne tous les titres
        /// </summary>
        /// <returns>Tous les titres contenus dans le set de données</returns>
        public IEnumerable<Titre> FindAll();

        /// <summary>
        /// Incrémente le nombre de lectures d'un titre
        /// </summary>
        /// <param name="titre">Titre à modifier</param>
        public void IncrementNbLectures(Titre titre);

        /// <summary>
        /// Incrémente le nombre de likes d'un titre
        /// </summary>
        /// <param name="titre">Titre à modifier</param>
        public void IncrementNbLikes(Titre titre);

        /// <summary>
        /// Recherche les titres concernant le mot recherché
        /// </summary>
        /// <param name="mot">Mots clés</param>
        /// <returns>Titres correspondants</returns>
        public IEnumerable<Titre> Search(string mot);

        /// <summary>
        /// Recherche les titres en fonction des styles
        /// </summary>
        /// <param name="mot">Mots clés</param>
        /// <returns>Titres correspondants</returns>
        public IEnumerable<Titre> SearchByStyle(string mot);

        /// <summary>
        /// Obtenir les x titres les plus populaires
        /// </summary>
        /// <param name="date">Date limite de la recherche</param>
        /// <param name="limit">limite de resultats</param>
        /// <returns>Titres les plus populaires</returns>
        public IEnumerable<Titre> GetPopular(DateTime date, int limit);

        /// <summary>
        /// Met à jour un titre
        /// </summary>
        /// <param name="titre">Titre mis à jour</param>
        public void Update(Titre titre);

        /// <summary>
        /// Retourne une fourchette de titres
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        public IEnumerable<Titre> Take(int index, int length);


        /// <summary>
        /// Permet de sauvegarder les changements dans la base de données
        /// </summary>
        public void Save();
    }
}
