using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.Repository.Contracts
{
    public interface ICommentaireRepository
    {
        /// <summary>
        /// Ajoute un commentaire dans le set de données
        /// </summary>
        /// <param name="commentaire">Commentaire à ajouter</param>
        public void Add(Commentaire commentaire);

        /// <summary>
        /// Supprime un commentaire présent dans le set de données
        /// </summary>
        /// <param name="commentaire">Commentaire à supprimer</param>
        public void Delete(Commentaire commentaire);

        /// <summary>
        /// Retourne tout les commentaires présents dans le set de données
        /// </summary>
        /// <returns>Commentaires contenus dans le set de données</returns>
        public IEnumerable<Commentaire> FindAll();

        /// <summary>
        /// Renvoie le commentaire correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID du commentaire recherché</param>
        /// <returns>Commentaire correspondant</returns>
        public Commentaire Find(int id);

        /// <summary>
        /// Retourne le nombre de commentaires présent dans le set de données
        /// </summary>
        /// <returns>Nombre de commentaires présents dans le set de données</returns>
        public int Count();

        /// <summary>
        /// Retourne une fourchette de commentaires
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        public IEnumerable<Commentaire> Take(int index, int length);

    }
}
