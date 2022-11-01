# Problème relations

Juste après avoir réussi à créer les différentes implémentations de notre
DbContext, nous nous sommes heurter à un problème majeur : la binavigabilité
entre nos objets. En effet, sur nos données bouchonnées (StaticFactory),
la binavigabilité fonctionnait ainsi:

- Titre -> Artistes -> Titres associés à cet artiste
- Artistes -> Titres -> Artiste associé à ce titre
- Styles -> Titres -> Styles associés
- Titres -> Styles -> Titres associés
- Commentaires -> Titre -> Commentaires associés
- Titres -> Commentaires -> Titres associés

Cet binavigabilité était établie et rafraîchie grâce à la méthode 
_UpdateBinavigabilite()_ implémentée dans la StaticFactory.
Afin d'implémenter une méthode similaire dans nos DbFactory, nous avions
premièrement éffectué des requêtes Linq multiples, peu optimisées et 
inutiles, car nous n'utilisions pas tout le potentiel d'Entity Framework Core.

Lorsque nous avions donc décidé de faire la transition vers l'utilisation
d'EF Core à son plein potentiel, impossible d'établir la binavigabilité
suite à des erreurs de clé étrangères. Celles-ci étaient mal configurées
(DataIndentation incorrectes) et donc EF Core ne pouvait pas établir les
différentes liaisons possibles. Après pas mal de recherches, nous avons 
donc réécrit le tout en faisant appel à la Fluent API, et donc une fois les
liaisons correctement définies, grâce aux commandes _.Include()_ et
_.ThenInclude()_, nous avons pû correctement utiliser EF Core, de 
façon bien plus optimisée, et surtout correctement fonctionnelle.

## Sommaire

[Retour à la liste d'erreurs](erreurs.md)

[Retour au début du rapport](Rapport-equipe-1.md)
