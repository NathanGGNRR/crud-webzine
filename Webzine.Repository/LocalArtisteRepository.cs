using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class LocalArtisteRepository : IArtisteRepository
    {
        /// <summary>
        /// Cherche un artiste qui contient le parametre mot (n'est pas case sensitive)
        /// </summary>
        /// <param name="mot">Nom d'artiste</param>
        /// <returns>Liste d'artiste qui contienne le paramétre</returns>
        IEnumerable<Artiste> IArtisteRepository.Search(string mot)
        {
            return StaticFactory.Artistes.Where(a => a.Nom.Contains(mot, System.StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Ajoute un artiste dans le set de données
        /// </summary>
        /// <param name="artiste">Artiste à ajouter</param>
        void IArtisteRepository.Add(Artiste artiste)
        {
            artiste.IdArtiste = StaticFactory.Artistes.Max(x => x.IdArtiste) + 1;

            // Application de la binavigabilité
            artiste.Titres = StaticFactory.Titres.Where(t => t.Artiste.Nom == artiste.Nom).ToList();

            // Ajout de l'Artiste dans le set de données
            StaticFactory.Artistes.Add(artiste);
        }

        /// <summary>
        /// Supprime un artiste du set de données
        /// </summary>
        /// <param name="artiste">Artiste à supprimer</param>
        void IArtisteRepository.Delete(Artiste artiste)
        {
            // Si l'artiste existe dans le set, suppression
            if (StaticFactory.Artistes.Contains(artiste))
            {
                StaticFactory.Artistes.Remove(artiste);
            }
        }

        /// <summary>
        /// Envoie le nombre d'artistes contenus dans le set de données
        /// </summary>
        /// <returns>Nombre d'artistes dans le set</returns>
        int IArtisteRepository.Count()
        {
            return StaticFactory.Artistes.Count;
        }

        /// <summary>
        /// Renvoie l'artiste correspondant à l'ID donné
        /// </summary>
        /// <param name="id">ID de l'artiste recherché</param>
        /// <returns>Artiste correspondant</returns>
        Artiste IArtisteRepository.Find(int id)
        {
            return StaticFactory.Artistes.FirstOrDefault(x => x.IdArtiste == id);
        }


        /// <summary>
        /// Renvoie l'artiste correspondant au nom donné
        /// </summary>
        /// <param name="name">Nom de l'artiste recherché</param>
        /// <returns>Artiste correspondant</returns>
        Artiste IArtisteRepository.Find(string name)
        {
            return StaticFactory.Artistes.FirstOrDefault(x => x.Nom == name);
        }

        /// <summary>
        /// Récupère tout les artistes contenus dans le set de données
        /// </summary>
        /// <returns>Artistes présents dans le set</returns>
        IEnumerable<Artiste> IArtisteRepository.FindAll()
        {
            StaticFactory.UpdateBinavigabilite();
            return StaticFactory.Artistes;
        }

        /// <summary>
        /// Retourne une fourchette d'artistes
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Artiste> IArtisteRepository.Take(int index, int length)
        {
            StaticFactory.UpdateBinavigabilite();
            return StaticFactory.Artistes.OrderBy(x => x.Nom).ToList().GetRange(index, length);
        }

        /// <summary>
        /// Met à jour un artiste dans le set de données
        /// </summary>
        /// <param name="artiste">Artiste modifié</param>
        void IArtisteRepository.Update(Artiste artiste)
        {
            // Si l'artiste existe bien
            if (!StaticFactory.Artistes.FirstOrDefault(a => a.IdArtiste == artiste.IdArtiste).Equals(default(Artiste)))
            {
                StaticFactory.Artistes.FirstOrDefault(a => a.IdArtiste == artiste.IdArtiste).Biographie = artiste.Biographie;
                StaticFactory.Artistes.FirstOrDefault(a => a.IdArtiste == artiste.IdArtiste).Nom = artiste.Nom;
                StaticFactory.Artistes.FirstOrDefault(a => a.IdArtiste == artiste.IdArtiste).Titres = artiste.Titres;

                StaticFactory.UpdateBinavigabilite();
            }
        }
    }
}
