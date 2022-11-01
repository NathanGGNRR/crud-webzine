using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class DbStyleRepository : IStyleRepository
    {
        private EntitiesContext.WebzineDbContext _dbContext = new EntitiesContext.WebzineDbContext();

        /// <summary>
        /// Ajoute un style dans le set de données
        /// </summary>
        /// <param name="style">Style à ajouter</param>
        void IStyleRepository.Add(Style style)
        {
            _dbContext.Styles.Add(style);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Supprime un style dans le set de données
        /// </summary>
        /// <param name="style">Style à supprimer</param>
        void IStyleRepository.Delete(Style style)
        {
            _dbContext.Styles.Remove(style);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Récupère le style correspondant un ID donné
        /// </summary>
        /// <param name="id">ID du style recherché</param>
        /// <returns>Style correspondant</returns>
        Style IStyleRepository.Find(int id)
        {
            return _dbContext.Styles.Where(s => s.IdStyle == id).Include(s => s.LienStyle).ThenInclude(l => l.Titre).ThenInclude(t => t.Artiste).FirstOrDefault();
        }

        /// <summary>
        /// Retourne le style correspondant au nom donné
        /// </summary>
        /// <param name="name">Identifiant du style demandé</param>
        Style IStyleRepository.FindByName(string name)
        {
            return _dbContext.Styles.AsNoTracking().Where(s => s.Libelle == name).Include(s => s.LienStyle).ThenInclude(l => l.Titre).ThenInclude(t => t.Artiste).FirstOrDefault();
        }

        /// <summary>
        /// Retourne tout les styles présents dans le set de données
        /// </summary>
        /// <returns>Styles présents dans le set de données</returns>
        IEnumerable<Style> IStyleRepository.FindAll()
        {
            try
            {
                return _dbContext.Styles.ToList();
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// Met un jour un style présent dans le set de données
        /// </summary>
        /// <param name="style">Stylé modifié</param>
        void IStyleRepository.Update(Style style)
        {
            _dbContext.Entry(_dbContext.Styles.FirstOrDefault(s => s.IdStyle == style.IdStyle)).CurrentValues.SetValues(style);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Retourne le nombre de styles présent dans le set de données
        /// </summary>
        /// <returns>Nombre de styles présents dans le set de données</returns>
        int IStyleRepository.Count()
        {
            return _dbContext.Styles.Count();
        }

        /// <summary>
        /// Retourne une fourchette de styles
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Style> IStyleRepository.Take(int index, int length)
        {
            return _dbContext.Styles.OrderBy(x => x.Libelle.ToLower()).Skip(index).Take(length);
        }
    }
}
