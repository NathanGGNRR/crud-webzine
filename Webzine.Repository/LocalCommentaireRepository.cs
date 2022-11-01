using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class LocalCommentaireRepository : ICommentaireRepository
    {
        /// <summary>
        /// Ajoute un commentaire dans le set de données
        /// </summary>
        /// <param name="commentaire">Commentaire à ajouter</param>
        void ICommentaireRepository.Add(Commentaire commentaire)
        {
            commentaire.IdCommentaire = StaticFactory.Commentaires.Max(x => x.IdCommentaire) + 1;

            // Ajout du commentaire dans le set de données
            StaticFactory.Commentaires.Add(commentaire);

            // Application de la binavigabilité
            StaticFactory.UpdateBinavigabilite();
        }

        /// <summary>
        /// Supprime un commentaire présent dans le set de données
        /// </summary>
        /// <param name="commentaire">Commentaire à supprimer</param>
        void ICommentaireRepository.Delete(Commentaire commentaire)
        {
            // Si le commentaire existe dans le set, suppression
            if (StaticFactory.Commentaires.Contains(commentaire))
            {
                StaticFactory.Commentaires.Remove(commentaire);
            }
        }

        /// <summary>
        /// Retourne tout les commentaires présents dans le set de données
        /// </summary>
        /// <returns>Commentaires contenus dans le set de données</returns>
        IEnumerable<Commentaire> ICommentaireRepository.FindAll()
        {
            return StaticFactory.Commentaires;
        }

        /// <summary>
        /// Renvoie le commentaire correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID du commentaire recherché</param>
        /// <returns>Commentaire correspondant</returns>
        Commentaire ICommentaireRepository.Find(int id)
        {
            return StaticFactory.Commentaires.FirstOrDefault(x => x.IdCommentaire == id);
        }

        /// <summary>
        /// Retourne le nombre de commentaires présent dans le set de données
        /// </summary>
        /// <returns>Nombre de commentaires présents dans le set de données</returns>
        int ICommentaireRepository.Count()
        {
            return StaticFactory.Commentaires.Count;
        }

        /// <summary>
        /// Retourne une fourchette de commentaires
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Commentaire> ICommentaireRepository.Take(int index, int length)
        {
            StaticFactory.UpdateBinavigabilite();
            return StaticFactory.Commentaires.OrderByDescending(x => x.DateCreation).ToList().GetRange(index, length);
        }

    }
}
