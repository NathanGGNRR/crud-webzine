# Configuration:
 
La configuration de notre application ce trouve dans le fichier **appsettings.json** du projet **Webzine.WebApplication**

## Paramètres :

- _DataSource_ : Permet de choisir quelle est la source de données utilisée pour seeder notre base de données,
  il peut accepter en paramètres : **factory** (utilise le jeu de données bouchonné),
 **local** (utiliser des données locales pour seeder la base de données),
**spotify** (seed la base de données avec des données de spotify)

- _DatabaseType_ : Permet de sélectionner le type de base de données à utiliser.
  Accepte en paramètres : **sqlite** (utilise une base de données SQLite, fichier WebzineDatabase.db à la racine de Webzine.WebApplication),
    ou sqlserver (LocalDB SQL Server, non fonctionnel pour le moment)

- _SpotifyPlaylistId_ : Identifiant Spotify de la playlist à importer (lors du seed de la database)

- _displayAdminPage_ : Nombre de pages visibles dans la pagination de la partie administration

- _lengthAdminPage_ : Nombre de resultats visibles par page sur la partie administration

- _lengthPage_ : Nombre de resultats visibles sur une page

- _lengthPop_ : Nombre de titres populaires visible sur la page d'accueil

- _lengthStylePage_ : Nombre de styles visibles sur la page des styles

- _ResetDb_ : Prend **true** pour activer le reset des données de la base au démarage des l'application,
  **false** pour passer en persistance 
    

