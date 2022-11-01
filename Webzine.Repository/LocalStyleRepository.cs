using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class LocalStyleRepository : IStyleRepository
    {
        /// <summary>
        /// Retourne le style correspondant au nom donné
        /// </summary>
        /// <param name="name">Identifiant du style demandé</param>
        public Style FindByName(string name)
        {
            Style style = StaticFactory.Styles.FirstOrDefault(s => s.Libelle == name);

            style.LienStyle = StaticFactory.LienStyles.Where(ls => style.IdStyle == ls.IdStyle).ToList();
            style.LienStyle.ToList().ForEach(ls => ls.Titre = StaticFactory.Titres.FirstOrDefault(t => t.IdTitre == ls.IdTitre));

            return style;
        }

        /// <summary>
        /// Ajoute un style dans le set de données
        /// </summary>
        /// <param name="style">Style à ajouter</param>
        void IStyleRepository.Add(Style style)
        {
            style.IdStyle = StaticFactory.Styles.Max(x => x.IdStyle) + 1;

            // Ajout de l'Artiste dans le set de données
            StaticFactory.Styles.Add(style);
        }

        /// <summary>
        /// Supprime un style dans le set de données
        /// </summary>
        /// <param name="style">Style à supprimer</param>
        void IStyleRepository.Delete(Style style)
        {
            // Si le style existe dans le set, suppression
            if (StaticFactory.Styles.Contains(style))
            {
                StaticFactory.LienStyles.Where(x => x.IdStyle == style.IdStyle).ToList().ForEach(ls => StaticFactory.LienStyles.Remove(ls));
                StaticFactory.Styles.Remove(style);
                StaticFactory.UpdateBinavigabilite();
            }
        }

        /// <summary>
        /// Récupère le style correspondant un ID donné
        /// </summary>
        /// <param name="id">ID du style recherché</param>
        /// <returns>Style correspondant</returns>
        Style IStyleRepository.Find(int id)
        {
            return StaticFactory.Styles.FirstOrDefault(x => x.IdStyle == id);
        }

        /// <summary>
        /// Retourne tout les styles présents dans le set de données
        /// </summary>
        /// <returns>Styles présents dans le set de données</returns>
        IEnumerable<Style> IStyleRepository.FindAll()
        {
            StaticFactory.UpdateBinavigabilite();
            return StaticFactory.Styles;
        }

        /// <summary>
        /// Met un jour un style présent dans le set de données
        /// </summary>
        /// <param name="style">Stylé modifié</param>
        void IStyleRepository.Update(Style style)
        {
            // Si le style existe bien
            if (!StaticFactory.Styles.FirstOrDefault(a => a.IdStyle == style.IdStyle).Equals(default(Style)))
            {
                StaticFactory.Styles.FirstOrDefault(a => a.IdStyle == style.IdStyle).Libelle = style.Libelle;

                StaticFactory.UpdateBinavigabilite();
            }
        }

        /// <summary>
        /// Retourne le nombre de styles présent dans le set de données
        /// </summary>
        /// <returns>Nombre de styles présents dans le set de données</returns>
        int IStyleRepository.Count()
        {
            return StaticFactory.Styles.Count;
        }

        /// <summary>
        /// Retourne une fourchette de styles
        /// </summary>
        /// <param name="index">Début de la fourchette</param>
        /// <param name="length">Taille de la fourchette</param>
        IEnumerable<Style> IStyleRepository.Take(int index, int length)
        {
            StaticFactory.UpdateBinavigabilite();
            return StaticFactory.Styles.OrderBy(x => x.Libelle).ToList().GetRange(index, length);
        }
    }
}
