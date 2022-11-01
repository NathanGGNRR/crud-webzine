# Répartition des tâches
Comme dit précédement, Nathan était chef de projet. Dans les premiers jours, nous avons tous défini le projet que nous avons découpé en 3 partie:
- Partie IHM (Interface homme machine) / visuelles
- Partie accès/stockage des données
- Partie back-end

Ces trois parties sont découpés en plusieurs features qui elles mêmes sont découpés en tâches. ([VOIR LE DASHBOARD](https://dev.azure.com/DiiaZik/Webzine/_dashboards/dashboard/1bee80fa-a9fd-49be-9192-bb48fdea16ee))

Après avoir une base solide pour pouvoir commencer à être efficace dans la gestion du projet, nous avons décidé d'affecter des tâches à chaques membres dans toutes les features ou les parties.
Un exemple: tous le monde à fait le visuel d'une page.
Pendant 2 semaines nous avons suivi ce modèle. Dès qu'une personne terminais son travail, il mettait à jour le repository pour que les autres puissent reprendre son travail et s'en appuyer pour
continuer son travail.
Due à quelques erreurs qui nous ont ralenti et qui vous seront présenté un peu plus tard dans ce rapport, ce modèle n'a pas tenu et nous nous sommes affecté des tâches sans tenir 
compte des tâches que nous avions déjà réalisées mais du temps qu'il nous reste. Par exemple, Nathan à réalisé le CRUD Style la dernière semaine puis a enchainé sur les autres CRUD étant donné qu'il ne restais que quelques tâches et que 
les autres membres était occupés.


# Architecture de l'application

L'application est découpée en plusieurs parties / projets, 10 aux total. Elle est développée sous ASP.NET Core avec plusieurs packages:
- Faker (Insertion de fake data)
- Microsoft.AspNet.Mvc
- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation (Refresh automatique sans redémarrer la solution)
- Microsoft.EntityFrameworkCore (ORM)
- Microsoft.EntityFrameworkCore.Sqlite (Gestion BDD Sqlite)
- Microsoft.EntityFrameworkCore.SqlServer (Gestion BDD SQLServer)
- Newtonsoft.Json

 Nous avons décider d'utilisé le pattern MVC et le Code First.


## Partie WebApplication (WebApplication):
Ce projet est la partie principale de notre application, elle contient le concept MVC : Models/Views/Controllers.
Au démarrage de ce projet, l'application génére un serveur IIS (localhost) grâce au fichier Program.cs.
Puis instancie le fichier Startup.cs, qui lui s'occupe des injections de dépendances, des middlewares et des gestions des routes.
C'est ce fichier qui va déterminer qu'elle page sera démarrée en premier. Dans notre cas nous avons créer une route par défaut
avec le pattern `{controller=Home}/{action=Index}`. Ce pattern signifie que si dans l'url, aucune information n'est donnée (index),
c'est le controller Home qui est instancié et qui va afficher la vue index en éxecutant l'action Index.

Le MVC, comme son nom l'indique, permet de diviser son projet en trois parties. Le fonctionnement de ce modèle est simple.
Cependant sa mise en place est régie par plusieurs règles obligatoires. Tous les controllers doivent être placés dans le dossier
"Controllers" et doivent tous s'appeller "NomDeCeQueJeVeux**Controller**". Pour les views, le dossier racine doit être appelé "Views", puis 
chacunes de ces view doit avoir un dossier avec le nom du controller sans le suffixe controller. Dans ces dossiers, se trouvent des fichiers cshtml propre à la technologie Asp.Net Razor qui sont nommés
comme les actions basiques des controllers. Par exemple, action index avec vue index.cshtml.
Pour les modèles, nous avons décider de créer un second projet appelé Entity où se trouvent tous nos modèles (Titre, Artiste, etc ...).
Après avoir ajouté la référence dans le projet WebApplication, nous utilisons des ViewModels pour reprendre uniquement les informations nécessaires
dans l'affichage d'une vue. Avoir l'objet Artiste dans le Titre dans une vue n'est pas nécessaire, seul le nom de l'artiste l'est.
Pour faire le liens entre le modèle, le controller et la vue, la démarche est simple. Lorsque que le controller est instancié et lance la fonction appelée,
à l'interieur de l'action, on instancie un ViewModel avec les informations nécessaires obtenues grâce aux entités.
Dès lors que l'on à le ViewModel, il est possible de le passer en paramètre. Ensuite la vue doit spécifier le type du paramètre reçu grâce à cette ligne de code:
`@model TypeDuParamètre`. Avec ce code, il est possible de reprendre les propriétés de l'objet avec `@Model.NomPropriété`.

Il est possible de diviser son code en Areas, dossier Areas qui contient les dossiers Controllers/Views/ViewsModel.
Cela permet une meilleur visibilité et est bien plus maintenable. Par exemple, si l'on a une partie Administration, il nous faudrait un controller TitreController et un autre ManageTitreController, ce qui 
nous fait des titres très longs. Avec les areas, il est possible de nommer les controllers de la même façon.

### Partie components and layout

Dans la même optique d'avoir une meilleur maintenabilité et une meilleur clareté, nous avons créer des components et un layout.
Les composants doivent avoir des règles de nommages. Les ViewComponents doivent être dans le dossier ViewComponents. Leurs vues doivent êtres dans un dossier Components lui même
dans le dossier Views / Controller où la vue appelle le composant. Lors de l'appel de ce composant, il est possible de lui passer un paramètre.
Pour le layout, c'est une vue que l'on créer dans le dossier Views/Shared. Pour appeler ce layout dans une vue, il suffit 
de mettre:
`@{
    Layout = "_Layout";
}`
Le nom du layout doit commencer par un underscore.
Dans ce layout, se trouve le header / footer. Nous avons choisi d'utiliser Bootstrap (comme indiqué dans le cahier des charges), donc c'est dans le header et le footer que l'on ajoute les références.
Les librairies externes sont situées dans le dossier `wwwroot/lib/`. 

De plus, la WebApplication contient un fichier de configuration appsettings.json. Ce fichier permet de changer des variables
comme le nombre d'éléments affichés sur une page. 


### Partie API
Le client a souhaité pouvoir accéder aux données de l'extérieur. De ce fait, nous avons créé une WebAPI qui permet de reprendre / rajouter / éditer / supprimer des données.
Exemple : pour avoir tous les Titres, requête GET sur la route : `api/Titres`.
Pour avoir la liste des routes définies pour la WebAPI, vous pouvez vous référencer au cahier des charges.

## Partie Entity (Bibliothèque de classes):
Comme dit précédemment, ce projet contient toutes les classes:
- Titre
- Style
- Artiste
- Commentaire

## Partie UnitTest (MSTest Core):
Ce projet permet de tester toutes les entités et de savoir si elles répondent à l'attente du client.
Par exemple, le client voulait que le nom de l'artiste soit toujours renseigné à sa création et à l'édition.
En Asp.Net Core, il existe les DataAnnotations qui permettre de renseigner plusieurs conditions:
- `Required` (doit être renseigné, peut être suivi de (`(ErrorMessage = "Le nom de l'artiste doit être renseigné !")`)
- `MaxLength(x)` (x correspond à la taille)
- `MinLength(x)`

## Partie Common (Bibliothèque de classes):
Ce projet contient toutes les classes qui peuvent être considérées comme utilisable partout.
Par exemple, nous avons créé la classe StringExtension qui permet de couper une chaine de caractère avec une taille spécifiée puis
de rajouter trois points à la fin (utilisée pour afficher un aperçu d'une chronique sur la page d'accueil).

## Partie Repository (Bibliothèque de classes):

Les repository servent essentiellement à accéder au données et à les gérer, c'est à dire pouvoir en rajouter, modifier et supprimer.
Cette partie est divisée en 2, une pour gérer les données bouchonnées (statiques) et une pour les données stockées dans une base de données.
Ces deux parties ont exactement les mêmes fonctions (signatures) mais ne font pas les même actions. Le local influe directement sur la classe `StaticFactory`
tandis que le DbRepository influe sur la base de données (via notre DbContext).
Cette base de données est rempli de deux façons différentes, soit avec les données de la classe StaticFactory,
soit avec une playlist Spotify (appel à l'API Spotify pour importer les données depuis une Playlist).
C'est essentiellement les controllers qui appellent ses Repository.
Cependant, pour pouvoir accéder à ses fonctions, il faut un contrat de confiance: une interface pour chaque repository.

## Parte Repository.Contracts (Bibliothèque de classes):

Composé du nombre total de repository divisé par deux de la partie Repository, ce projet contient uniquement
les interfaces des repository. Grâce à ces interfaces, les controllers peuvent avoir accès aux fonctions, néanmoins
il faut quand même faire de l'injection de dépendances pour chaque interface.

## Partie EntitiesContext (Bibliothèque de classes):
Comme dit précédement, il y a trois modes de gestion des données: une local où l'on reprend directement les données depuis la classe `StaticFactory`.
Les deux modes qui nous intéressent et qui concernent cette partie sont le mode local DB et le spotify.
C'est au lancement de l'application dans la classe `Startup.cs` que l'on choisi.
Dans les deux cas, la premier action est d'instancier la classe `WebzineDbContext.cs`.
Elle va donc stocker dans un fichier WebzineDatabase.db (Base de données SQLite) toutes les informations avec tous les liens.
Ensuite, soit c'est le `LocalSeeder` qui est appelé et va donc prendre les données statics, soit c'est `SpotifySeeder` qui
lui va appeler l'API Spotify avec toutes les informations nécessaires.

## Partie Business (Bibliothèque de classes) & Partie Business.Contracts (Bibliothèque de classes):
Cette partie est destinée au méthodes client, c'est une couche service. Méthodes qui sont uniquement pour l'expérience client, comme notamment les likes.
Cette partie est censé être indépendante et peut donc être utilisé comme une librairie.
C'est cette partie qui appelle le repository dans les projets normalement structurés.
Les controllers feraient donc appel aux classes des services et non directement aux repository.
Tous comme les repository, cette couche à besoin d'un contrat de confiance afin de fonctionner (interfaces).

## Prochaine partie

[Erreurs rencontrées](erreurs.md)

[Retour au début du rapport](Rapport-equipe-1.md)



