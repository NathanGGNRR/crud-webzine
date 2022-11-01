using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class DbCommentaireRepository : ICommentaireRepository
    {
        private EntitiesContext.WebzineDbContext _dbContext = new EntitiesContext.WebzineDbContext();

        /// <summary>
        /// Ajoute un commentaire dans le set de données
        /// </summary>
        /// <param name="commentaire">Commentaire à ajouter</param>
        void ICommentaireRepository.Add(Commentaire commentaire)
        {
            commentaire.Titre = _dbContext.Titres.FirstOrDefault(x => x.IdTitre == commentaire.IdTitre);
            _dbContext.Commentaires.Add(commentaire);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Supprime un commentaire présent dans le set de données
        /// </summary>
        /// <param name="commentaire">Commentaire à supprimer</param>
        void ICommentaireRepository.Delete(Commentaire commentaire)
        {
            _dbContext.Commentaires.Remove(commentaire);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Renvoie le commentaire correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID du commentaire recherché</param>
        /// <returns>Commentaire correspondant</returns>
        Commentaire ICommentaireRepository.Find(int id)
        {
            return _dbContext.Commentaires.Where(x => x.IdCommentaire == id).Include(c => c.Titre).ThenInclude(c => c.Artiste).FirstOrDefault();
        }

        /// <summary>
        /// Retourne tout les commentaires présents dans le set de données
        /// </summary>
        /// <returns>Commentaires contenus dans le set de données</returns>
        IEnumerable<Commentaire> ICommentaireRepository.FindAll()
        {
            return _dbContext.Commentaires.Include(c => c.Titre).ThenInclude(c => c.Artiste).ToList();
        }

        /// <summary>
        /// Retourne le nombre de commentaires présent dans le set de données
        /// </summary>
        /// <returns>Nombre de commentaires présents dans le set de données</returns>
        int ICommentaireRepository.Count()
        {
            return _dbContext.Commentaires.Count();
        }

        /// <summary>
        /// Retourne une fourchette de commentaires
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Commentaire> ICommentaireRepository.Take(int index, int length)
        {
            return _dbContext.Commentaires.OrderByDescending(x => x.DateCreation).Skip(index).Take(length).Include(c => c.Titre).ThenInclude(c => c.Artiste).ToList();
        }
    }
}
