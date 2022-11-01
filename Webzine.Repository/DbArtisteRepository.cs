using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class DbArtisteRepository : IArtisteRepository
    {
        private EntitiesContext.WebzineDbContext _dbContext = new EntitiesContext.WebzineDbContext();

        /// <summary>
        /// Cherche un artiste qui contient le parametre mot (n'est pas case sensitive)
        /// </summary>
        /// <param name="mot">Nom d'artiste</param>
        /// <returns>Liste d'artiste qui contienne le paramétre</returns>
        IEnumerable<Artiste> IArtisteRepository.Search(string mot)
        {
            return _dbContext.Artistes.Where(a => a.Nom.ToLower().Contains(mot.ToLower())).ToList();
        }

        /// <summary>
        /// Ajoute un artiste dans le set de données
        /// </summary>
        /// <param name="artiste">Artiste à ajouter</param>
        void IArtisteRepository.Add(Artiste artiste)
        {
            _dbContext.Artistes.Add(artiste);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Supprime un artiste du set de données
        /// </summary>
        /// <param name="artiste">Artiste à supprimer</param>
        void IArtisteRepository.Delete(Artiste artiste)
        {
            _dbContext.Artistes.Remove(artiste);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Envoie le nombre d'artistes contenus dans le set de données
        /// </summary>
        /// <returns>Nombre d'artistes dans le set</returns>
        int IArtisteRepository.Count()
        {
            return _dbContext.Artistes.Count();
        }

        /// <summary>
        /// Renvoie l'artiste correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID de l'artiste recherché</param>
        /// <returns>Artiste correspondant</returns>
        Artiste IArtisteRepository.Find(int id)
        {
            return _dbContext.Artistes.Where(a => a.IdArtiste == id).Include(a => a.Titres).FirstOrDefault();
        }

        /// <summary>
        /// Renvoie l'artiste correspondant au nom donné
        /// </summary>
        /// <param name="name">Nom de l'artiste recherché</param>
        /// <returns>Artiste correspondant</returns>
        Artiste IArtisteRepository.Find(string name)
        {
            return _dbContext.Artistes.Where(a => a.Nom == name).Include(a => a.Titres).FirstOrDefault();
        }

        /// <summary>
        /// Récupère tout les artistes contenus dans le set de données
        /// </summary>
        /// <returns>Artistes présents dans le set</returns>
        IEnumerable<Artiste> IArtisteRepository.FindAll()
        {
            return _dbContext.Artistes.ToList();
        }

        /// <summary>
        /// Retourne une fourchette d'artistes
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Artiste> IArtisteRepository.Take(int index, int length)
        {
            return _dbContext.Artistes.OrderBy(x => x.Nom.ToLower()).Skip(index).Take(length);
        }

        /// <summary>
        /// Met à jour un artiste dans le set de données
        /// </summary>
        /// <param name="artiste">Artiste modifié</param>
        void IArtisteRepository.Update(Artiste artiste)
        {
            _dbContext.Entry(_dbContext.Artistes.FirstOrDefault(a => a.IdArtiste == artiste.IdArtiste)).CurrentValues.SetValues(artiste);
            _dbContext.SaveChanges();
        }
    }
}
