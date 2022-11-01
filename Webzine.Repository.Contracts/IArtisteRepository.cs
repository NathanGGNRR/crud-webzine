using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.Repository.Contracts
{
    public interface IArtisteRepository
    {
        /// <summary>
        /// Ajoute un artiste dans le set de données
        /// </summary>
        /// <param name="artiste">Artiste à ajouter</param>
        public void Add(Artiste artiste);

        /// <summary>
        /// Supprime un artiste du set de données
        /// </summary>
        /// <param name="artiste">Artiste à supprimer</param>
        public void Delete(Artiste artiste);

        /// <summary>
        /// Envoie le nombre d'artistes contenus dans le set de données
        /// </summary>
        /// <returns>Nombre d'artistes dans le set</returns>
        public int Count();

        /// <summary>
        /// Renvoie l'artiste correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID de l'artiste recherché</param>
        /// <returns>Artiste correspondant</returns>
        public Artiste Find(int id);

        /// <summary>
        /// Renvoie l'artiste correspondant au nom donné
        /// </summary>
        /// <param name="name">Nom de l'artiste recherché</param>
        /// <returns>Artiste correspondant</returns>
        public Artiste Find(string name);

        /// <summary>
        /// Récupère tout les artistes contenus dans le set de données
        /// </summary>
        /// <returns>Artistes présents dans le set</returns>
        public IEnumerable<Artiste> FindAll();

        /// <summary>
        /// Retourne une fourchette d'artistes
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        public IEnumerable<Artiste> Take(int index, int length);

        /// <summary>
        /// Met à jour un artiste dans le set de données
        /// </summary>
        /// <param name="artiste">Artiste modifié</param>
        public void Update(Artiste artiste);

        /// <summary>
        /// Cherche un artiste qui contient le parametre mot (n'est pas case sensitive)
        /// </summary>
        /// <param name="mot">Nom d'artiste</param>
        /// <returns>Liste d'artiste qui contienne le paramétre</returns>
        public IEnumerable<Artiste> Search(string mot);
    }
}
