# Problème Unit Of Work

Pour implémenter la Business Layer à notre solution, nous avons essayé d'utiliser le pattern UnitOfWork.
Celui-ci nous aurrait permi entre autres de gérer l'application des modifications sur la base de données
(méthode *SaveChanges()* du DbContext), depuis la couche métier et non plus depuis les repository.


Nous avons cepandant pas réussi à mettre en place une implémentation de IUnitOfWork depuis le *Startup.cs*.
La difficulté étant de donner le même DbContext que celui implanté dans le Startup, lors de la configuration
des services et donc de l'injection de dépendances.

Pour palier à ce problème, nous avons créer une méthode *Save()* dans notre Repository, qui fait appel à la 
méthode *SaveChanges()* de notre DbContext.

Grâce à cette solution, nous pouvons donc correctement appliquer les modifications en base depuis notre BAL,
et nous avons donc pu réussir à implémenter l'incrémentation du nombre de vues, ainsi que l'incrémentation 
du nombre de *Likes*, en mode Database, sur nos titre, depuis la BAL.

## Sommaire

[Retour à la liste d'erreurs](erreurs.md)

[Retour au début du rapport](Rapport-equipe-1.md)