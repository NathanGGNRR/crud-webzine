using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;

namespace Webzine.EntitiesContext
{
    /// <summary>
    /// Insert des données dans la base de données sélectionnée
    /// </summary>
    public class LocalSeeder
    {
        private List<Style> _styles;
        private List<Titre> _titres;
        private List<Artiste> _artistes;
        private List<Commentaire> _commentaires;
        private List<LienStyle> _lienStyles;
        private WebzineDbContext _dbContext;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="context">DbContext de la base de données à traiter</param>
        public LocalSeeder(WebzineDbContext context)
        {
            _dbContext = context;
            _styles = new List<Style>();
            _titres = new List<Titre>();
            _artistes = new List<Artiste>();
            _commentaires = new List<Commentaire>();
            _lienStyles = new List<LienStyle>();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="context">DbContext de la base de données à traiter</param>
        /// <param name="styles">Styles à ajouter</param>
        /// <param name="titres">Titres à ajouter</param>
        /// <param name="artistes">Artistes à ajouter</param>
        /// <param name="commentaires">Commentaires à ajouter</param>
        public LocalSeeder(WebzineDbContext context, List<Style> styles, List<Titre> titres, List<Artiste> artistes, List<Commentaire> commentaires, List<LienStyle> lienStyles)
        {
            _dbContext = context;
            _styles = styles;
            _titres = titres;
            _artistes = artistes;
            _commentaires = commentaires;
            _lienStyles = lienStyles;
        }

        /// <summary>
        /// Charge les données envoyées dans le constructeur dans les tables
        /// Ne sauvegarde pas les modifications
        /// </summary>
        public void LoadData()
        {
            _styles.ForEach(style =>
            {
                style.LienStyle = _lienStyles.Where(ls => ls.IdStyle == style.IdStyle).ToList();
            });

            _artistes.ForEach(artist =>
            {
                artist.Titres = _titres.Where(t => t.Artiste.Nom == artist.Nom).ToList();
            });

            _titres.ForEach(titre => titre.Commentaires = _commentaires.Where(x => x.IdTitre == titre.IdTitre).ToList());
            _commentaires.ForEach(comment => comment.Titre = (_titres.FirstOrDefault(x => x.IdTitre == comment.IdTitre)));
            _styles.ForEach(t => _dbContext.Styles.Add(t));

            _titres.ForEach(t =>
            {
                _dbContext.Titres.Add(t);
            });

            _artistes.ForEach(t => _dbContext.Artistes.Add(t));
            _commentaires.ForEach(t => _dbContext.Commentaires.Add(t));

            _lienStyles.ForEach(ls => _dbContext.Add(ls));
        }
    }
}
