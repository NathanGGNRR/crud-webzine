# Accès aux données du wwwroot

Au tout début du projet, lors de la découverte des technologies ASP.Net ainsi que de l'utilisation du
modèle de développement MVC (Model, View, Controller) ainsi que du début de la création de la version statique
du site (suivant méticuleusement la charte graphique demandée par le client), nous n'arrivions pas à utiliser
des ressources statiques, pourtant contenues dans le dossier *wwwroot*. Après plusieurs recherches, notamment
sur la documentation Microsoft, nous avons pû voir qu'il suffisait d'ajouter la configuration 
`app.UseStaticFiles();` dans le fichier *Startup.cs* afin d'autoriser l'accès aux fichiers contenus
dans le dossier *wwwroot*. Un oubli simple, nous ayant fait tout de même perdre un peu de temps.

## Sommaire

[Retour à la liste d'erreurs](erreurs.md)

[Retour au début du rapport](Rapport-equipe-1.md)