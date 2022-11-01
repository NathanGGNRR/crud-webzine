using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.Repository.Contracts
{
    public interface IStyleRepository
    {
        /// <summary>
        /// Ajoute un style dans le set de données
        /// </summary>
        /// <param name="style">Style à ajouter</param>
        public void Add(Style style);

        /// <summary>
        /// Supprime un style dans le set de données
        /// </summary>
        /// <param name="style">Style à supprimer</param>
        public void Delete(Style style);

        /// <summary>
        /// Récupère le style correspondant un ID donné
        /// </summary>
        /// <param name="id">ID du style recherché</param>
        /// <returns>Style correspondant</returns>
        public Style Find(int id);

        /// <summary>
        /// Retourne le style correspondant au nom donné
        /// </summary>
        /// <param name="name">Identifiant du style demandé</param>
        public Style FindByName(string name);

        /// <summary>
        /// Retourne tout les styles présents dans le set de données
        /// </summary>
        /// <returns>Styles présents dans le set de données</returns>
        public IEnumerable<Style> FindAll();

        /// <summary>
        /// Met un jour un style présent dans le set de données
        /// </summary>
        /// <param name="style">Stylé modifié</param>
        public void Update(Style style);

        /// <summary>
        /// Retourne le nombre de styles présent dans le set de données
        /// </summary>
        /// <returns>Nombre de styles présents dans le set de données</returns>
        public int Count();

        /// <summary>
        /// Retourne une fourchette de styles
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        public IEnumerable<Style> Take(int index, int length);
    }
}
