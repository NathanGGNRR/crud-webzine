# Introduction - équipe 1
 
Ce projet, d'une durée de 3 semaines, à eu pour but de nous apprendre
à développer une WebApplication ASP.Net Core, utilisant le principe 
du MVC (Model, View, Controller), ainsi que l'utilisation de l'ORM 
Entity Framework Core. Notre équipe fut constituée de :

- Nathan Gagniarre - Chef de projet
- Valentin Virot
- Raphaël Delcroix
- Clément Hugon 

Vous pourrez découvrir, au travers de ce rapport, nos différents choix 
techniques qui auront permit, au termes de ces 3 semaines, d'avoir une
application fonctionnelle, ainsi que les différents problèmes majeurs
que nous avons pû rencontrer. 

## Sujet

Le but principale de cette application web : créer un site internet qui
répertorie différentes chroniques musicales ! Celle-ci contient sur la
page de la chronique un lien d'écoute vers le titre concerné (que ce soit
hebergé sur YouTube ou encore Spotify), ainsi que des commentaires postés
par les différents utilisateurs du site. Les chroniques sont ainsi
répertoriées en fonction de la popularité de celles-ci (vues/likes).

Le tout est biensûr entièrement manageable depuis la partie administration,
depuis laquelle les chroniques, les artistes, les styles ou encore les
commentaires peuvent être modifiés/supprimés.

Toutes les données peuvent êtres stockées sur plusieurs support, que ce 
soit des données bouchonnées, une base de données SQLite ou une base de 
données SQL Server. Le tout étant implémenté par le biais d'interfaces,
de façon à pouvoir utiliser l'implémentation souhaitée.

Enfin, les données stockées en bases peuvent être soit seedées par le 
biais des données bouchonnées, soit par le biais de l'API de Spotify
(via une playlist).

## Prochaine partie

[Analyse et partie technique](analyse.md)

[Retour au début du rapport](Rapport-equipe-1.md)
