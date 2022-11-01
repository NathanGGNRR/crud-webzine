using System;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;

namespace Webzine.Repository
{
    /// <summary>
    /// Contient un set de données statiques, de façon à pouvoir tester sans base de données.
    /// </summary>
    public static class StaticFactory
    {
        public static List<Style> Styles = new List<Style>
            {
                new Style
                {
                    IdStyle = 1,
                    Libelle = "Rap"
                },
                new Style
                {
                    IdStyle = 2,
                    Libelle = "Pop"
                },
                new Style
                {
                    IdStyle = 3,
                    Libelle = "Funk"
                },
                new Style
                {
                    IdStyle = 4,
                    Libelle = "Dubstep"
                },
                new Style
                {
                    IdStyle = 5,
                    Libelle = "Rock"
                },
                new Style
                {
                    IdStyle = 6,
                    Libelle = "Jazz"
                },
                new Style
                {
                    IdStyle = 7,
                    Libelle = "K-POP"
                },
                new Style
                {
                    IdStyle = 8,
                    Libelle = "House"
                },
                new Style
                {
                    IdStyle = 9,
                    Libelle = "J-Pop"
                },
                new Style
                {
                    IdStyle = 10,
                    Libelle = "Classique"
                },
                new Style
                {
                    IdStyle = 74,
                    Libelle = "French"
                }
            };
        public static List<Artiste> Artistes = new List<Artiste>
            {
                new Artiste
                {
                    IdArtiste = 1,
                    Nom = "PNL",
                    Biographie = "Biographie de PNL.",
                    Titres = new List<Titre>()
                },

                new Artiste
                {
                    IdArtiste = 2,
                    Nom = "Angèle",
                    Biographie = "Biographie d'Angèle.",
                    Titres = new List<Titre>()
                },

                new Artiste
                {
                    IdArtiste = 3,
                    Nom = "Guizmo x Big Flo & Oli",
                    Biographie = "Biographie de Guizmo x Big Flo & Oli.",
                    Titres = new List<Titre>()
                },

                new Artiste
                {
                    IdArtiste = 74,
                    Nom = "Renaud",
                    Biographie = "Biographie de Renaud.",
                    Titres = new List<Titre>()
                },

                new Artiste
                {
                    IdArtiste = 75,
                    Nom = "ACDC",
                    Biographie = "Biographie d'ACDC.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 76,
                    Nom = "Tones x I",
                    Biographie = "Biographie de Tones x I.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 77,
                    Nom = "Twenty one pilots",
                    Biographie = "Biographie de Twenty one pilots.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 78,
                    Nom = "Larry x RK",
                    Biographie = "Biographie de Larry x RK.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 79,
                    Nom = "M.Pokora",
                    Biographie = "Biographie de M.Pokora.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 80,
                    Nom = "Vitaa",
                    Biographie = "Biographie de Vitaa.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 81,
                    Nom = "Slimane",
                    Biographie = "Biographie de Slimane.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 82,
                    Nom = "Gims",
                    Biographie = "Biographie de Gims.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 83,
                    Nom = "Rilès",
                    Biographie = "Biographie de Rilès.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 84,
                    Nom = "Drake",
                    Biographie = "Biographie de Drake.",
                    Titres = new List<Titre>()
                },
                new Artiste
                {
                    IdArtiste = 85,
                    Nom = "Future",
                    Biographie = "Biographie de Future.",
                    Titres = new List<Titre>()
                }
            };
        public static List<Titre> Titres = new List<Titre>
            {
                new Titre {
                    IdTitre = 1,
                    IdArtiste = Artistes.FirstOrDefault(art => art.Nom == "PNL").IdArtiste,
                    Artiste = Artistes.FirstOrDefault(art => art.Nom == "PNL"),
                    Duree = 248,
                    Libelle = "Au DD",
                    Chronique = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget tristique ante, vestibulum bibendum dui. Sed mattis urna nisi, nec maximus eros congue vel. In hac habitasse platea dictumst. Cras vestibulum justo ac metus cursus, non blandit leo molestie. Fusce fringilla turpis a metus pulvinar iaculis. Praesent placerat aliquam mi in posuere. Pellentesque tincidunt egestas placerat. Cras ac commodo nunc, a ullamcorper orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque vitae scelerisque urna. Nulla ac risus vitae sem volutpat consequat at a sapien. Ut mauris lacus, convallis nec luctus id, aliquam et lacus. Vivamus nec tortor justo. Donec iaculis mauris metus, accumsan consectetur justo accumsan et. Donec porta nec lectus ut efficitur. Suspendisse sit amet commodo risus. Donec fermentum rutrum lorem id tristique. Nunc faucibus, mi eget viverra mollis, tortor turpis consequat turpis, vel fringilla velit augue nec magna.",
                    UrlJaquette = "https://m.media-amazon.com/images/I/61iuG4fhYRL._SS500_.jpg",
                    UrlEcoute = "https://www.youtube.com/embed/BtyHYIpykN0",
                    DateCreation = new DateTime(2019, 4, 5),
                    DateSortie = new DateTime(2019, 4, 5),
                    NbLectures = 42,
                    NbLikes = 12,
                    Album = "Deux frères"
                },
                new Titre {
                    IdTitre = 2,
                    IdArtiste = Artistes.FirstOrDefault(art => art.Nom == "Angèle").IdArtiste,
                    Artiste = Artistes.FirstOrDefault(art => art.Nom == "Angèle"),
                    Duree = 190,
                    Libelle = "Balance ton quoi",
                    Chronique = "Chronique de Balance ton quoi d'Angèle.",
                    UrlJaquette = "https://images.genius.com/4a5d9a2884d8308142efdc9941aa5678.1000x1000x1.jpg",
                    UrlEcoute = "https://www.youtube.com/embed/Hi7Rx3En7-k",
                    DateCreation = new DateTime(2019, 4, 15),
                    DateSortie = new DateTime(2019, 4, 15),
                    NbLectures = 24,
                    NbLikes = 21,
                    Album = "Brol"
                },
                new Titre {
                    IdTitre = 3,
                    IdArtiste = Artistes.FirstOrDefault(art => art.Nom == "Guizmo x Big Flo & Oli").IdArtiste,
                    Artiste = Artistes.FirstOrDefault(art => art.Nom == "Guizmo x Big Flo & Oli"),
                    Duree = 212,
                    Libelle = "Pas du même monde",
                    Chronique = "Chronique de Pas du même monde de Guizmo x Big Flo & Oli.",
                    UrlJaquette = "https://t2.genius.com/unsafe/220x0/https%3A%2F%2Fimages.genius.com%2F0f01bb95d9099b43b4a54254752cb12b.500x500x1.jpg",
                    UrlEcoute = "https://www.youtube.com/embed/jINOBawMWMk",
                    DateCreation = new DateTime(2018, 7, 6),
                    DateSortie = new DateTime(2018, 7, 6),
                    NbLectures = 12,
                    NbLikes = 74,
                    Album = "Pas du même monde (feat. Bigflo & Oli)"
                },
                new Titre {
                    IdTitre = 4,
                    IdArtiste = Artistes.FirstOrDefault(art => art.Nom == "PNL").IdArtiste,
                    Artiste = Artistes.FirstOrDefault(art => art.Nom == "PNL"),
                    Duree = 286,
                    Libelle = "Naha",
                    Chronique = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget tristique ante, vestibulum bibendum dui. Sed mattis urna nisi, nec maximus eros congue vel. In hac habitasse platea dictumst. Cras vestibulum justo ac metus cursus, non blandit leo molestie. Fusce fringilla turpis a metus pulvinar iaculis. Praesent placerat aliquam mi in posuere. Pellentesque tincidunt egestas placerat. Cras ac commodo nunc, a ullamcorper orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque vitae scelerisque urna. Nulla ac risus vitae sem volutpat consequat at a sapien. Ut mauris lacus, convallis nec luctus id, aliquam et lacus. Vivamus nec tortor justo. Donec iaculis mauris metus, accumsan consectetur justo accumsan et. Donec porta nec lectus ut efficitur. Suspendisse sit amet commodo risus. Donec fermentum rutrum lorem id tristique. Nunc faucibus, mi eget viverra mollis, tortor turpis consequat turpis, vel fringilla velit augue nec magna.",
                    UrlJaquette = "https://i.pinimg.com/originals/4e/46/9e/4e469e36451fbdee714e3763c4105c8c.jpg",
                    UrlEcoute = "https://www.youtube.com/embed/IOwom_Gp__Q",
                    DateCreation = new DateTime(2016, 9, 15),
                    DateSortie = new DateTime(2016, 9, 15),
                    NbLectures = 42,
                    NbLikes = 12,
                    Album = "Naha (Single)"
                },
                new Titre {
                    IdTitre = 101,
                    IdArtiste = Artistes.FirstOrDefault(art => art.Nom == "PNL").IdArtiste,
                    Artiste = Artistes.FirstOrDefault(art => art.Nom == "PNL"),
                    Duree = 317,
                    Libelle = "A l'ammoniaque",
                    Chronique = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget tristique ante, vestibulum bibendum dui. Sed mattis urna nisi, nec maximus eros congue vel. In hac habitasse platea dictumst. Cras vestibulum justo ac metus cursus, non blandit leo molestie. Fusce fringilla turpis a metus pulvinar iaculis. Praesent placerat aliquam mi in posuere. Pellentesque tincidunt egestas placerat. Cras ac commodo nunc, a ullamcorper orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque vitae scelerisque urna. Nulla ac risus vitae sem volutpat consequat at a sapien. Ut mauris lacus, convallis nec luctus id, aliquam et lacus. Vivamus nec tortor justo. Donec iaculis mauris metus, accumsan consectetur justo accumsan et. Donec porta nec lectus ut efficitur. Suspendisse sit amet commodo risus. Donec fermentum rutrum lorem id tristique. Nunc faucibus, mi eget viverra mollis, tortor turpis consequat turpis, vel fringilla velit augue nec magna. La magistrate ne connais que la douleur.",
                    UrlJaquette = "https://m.media-amazon.com/images/I/61iuG4fhYRL._SS500_.jpg",
                    UrlEcoute = "https://www.youtube.com/embed/Vl-GJaitlNs",
                    DateCreation = new DateTime(2018, 6, 22),
                    DateSortie = new DateTime(2018, 6, 22),
                    NbLectures = 45000,
                    NbLikes = 142,
                    Album = "Deux frères"
                },
                new Titre {
                    IdTitre = 98,
                    IdArtiste = Artistes.FirstOrDefault(art => art.Nom == "Renaud").IdArtiste,
                    Artiste = Artistes.FirstOrDefault(art => art.Nom == "Renaud"),
                    Duree = 267,
                    Libelle = "Dès que le vent soufflera",
                    Chronique = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget tristique ante, vestibulum bibendum dui. Sed mattis urna nisi, nec maximus eros congue vel. In hac habitasse platea dictumst. Cras vestibulum justo ac metus cursus, non blandit leo molestie. Fusce fringilla turpis a metus pulvinar iaculis. Praesent placerat aliquam mi in posuere. Pellentesque tincidunt egestas placerat. Cras ac commodo nunc, a ullamcorper orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque vitae scelerisque urna. Nulla ac risus vitae sem volutpat consequat at a sapien. Ut mauris lacus, convallis nec luctus id, aliquam et lacus. Vivamus nec tortor justo. Donec iaculis mauris metus, accumsan consectetur justo accumsan et. Donec porta nec lectus ut efficitur. Suspendisse sit amet commodo risus. Donec fermentum rutrum lorem id tristique. Nunc faucibus, mi eget viverra mollis, tortor turpis consequat turpis, vel fringilla velit augue nec magna.",
                    UrlJaquette = "http://www.encyclopedisque.fr/images/imgdb/thumb250/54241.jpg",
                    UrlEcoute = "https://www.youtube.com/embed/mm7nGX193bo",
                    DateCreation = new DateTime(2020, 1, 7),
                    DateSortie = new DateTime(1983, 1, 1),
                    NbLectures = 16548,
                    NbLikes = 1244,
                    Album = "Morgane de toi"
                },
                new Titre {
                    IdTitre = 99,
                    IdArtiste = Artistes.FirstOrDefault(art => art.Nom == "ACDC").IdArtiste,
                    Artiste = Artistes.FirstOrDefault(art => art.Nom == "ACDC"),
                    Duree = 255,
                    Libelle = "Back In Black",
                    Chronique = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget tristique ante, vestibulum bibendum dui. Sed mattis urna nisi, nec maximus eros congue vel. In hac habitasse platea dictumst. Cras vestibulum justo ac metus cursus, non blandit leo molestie. Fusce fringilla turpis a metus pulvinar iaculis. Praesent placerat aliquam mi in posuere. Pellentesque tincidunt egestas placerat. Cras ac commodo nunc, a ullamcorper orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque vitae scelerisque urna. Nulla ac risus vitae sem volutpat consequat at a sapien. Ut mauris lacus, convallis nec luctus id, aliquam et lacus. Vivamus nec tortor justo. Donec iaculis mauris metus, accumsan consectetur justo accumsan et. Donec porta nec lectus ut efficitur. Suspendisse sit amet commodo risus. Donec fermentum rutrum lorem id tristique. Nunc faucibus, mi eget viverra mollis, tortor turpis consequat turpis, vel fringilla velit augue nec magna.",
                    UrlJaquette = "https://upload.wikimedia.org/wikipedia/commons/b/be/Acdc_backinblack_cover.jpg",
                    UrlEcoute = "https://www.youtube.com/embed/pAgnJDJN4VA",
                    DateCreation = new DateTime(2020, 1, 7),
                    DateSortie = new DateTime(1980, 7, 25),
                    NbLectures = 424,
                    NbLikes = 125,
                    Album = "Back In Black"
                }
            };
        public static List<LienStyle> LienStyles = new List<LienStyle> {
            new LienStyle
            {
                IdStyle = 1,
                IdTitre = 1
            },
            new LienStyle
            {
                IdStyle = 2,
                IdTitre = 2
            },
            new LienStyle
            {
                IdStyle = 1,
                IdTitre = 3
            },
            new LienStyle
            {
                IdStyle = 74,
                IdTitre = 3
            },
            new LienStyle
            {
                IdStyle = 1,
                IdTitre = 4
            },
            new LienStyle
            {
                IdStyle = 74,
                IdTitre = 98
            },
            new LienStyle
            {
                IdStyle = 5,
                IdTitre = 99
            },
            new LienStyle
            {
                IdStyle = 1,
                IdTitre = 101
            },
            new LienStyle
            {
                IdStyle = 1,
                IdTitre = 99
            },
            new LienStyle
            {
                IdStyle = 1,
                IdTitre = 98
            }
        };
        public static List<Commentaire> Commentaires = new List<Commentaire> {
            new Commentaire
            {
                Auteur = "Raphaël Delcroix",
                Contenu = "Metallica > all, QLF",
                DateCreation = new DateTime(2019, 12, 24),
                IdCommentaire = 1,
                IdTitre = 1,
                Titre = new Titre()
            },
            new Commentaire
            {
                Auteur = "Raphaël Delcroix",
                Contenu = "Metallica > all, QLF",
                DateCreation = new DateTime(2019, 12, 24),
                IdCommentaire = 2,
                IdTitre = 1,
                Titre = new Titre()
            },
            new Commentaire
            {
                Auteur = "Raphaël Delcroix",
                Contenu = "Metallica > all, QLF",
                DateCreation = new DateTime(2019, 12, 24),
                IdCommentaire = 3,
                IdTitre = 1,
                Titre = new Titre()
            },
            new Commentaire
            {
                Auteur = "Raphaël Delcroix",
                Contenu = "Metallica > all, QLF",
                DateCreation = new DateTime(2019, 12, 24),
                IdCommentaire = 4,
                IdTitre = 1,
                Titre = new Titre()
            },
            new Commentaire
            {
                Auteur = "Raphaël Delcroix",
                Contenu = "Metallica > all, QLF",
                DateCreation = new DateTime(2019, 12, 24),
                IdCommentaire = 5,
                IdTitre = 1,
                Titre = new Titre()
            },new Commentaire
            {
                Auteur = "Raphaël Delcroix",
                Contenu = "Metallica > all, QLF",
                DateCreation = new DateTime(2019, 12, 24),
                IdCommentaire = 6,
                IdTitre = 1,
                Titre = new Titre()
            }

        };

        /// <summary>
        /// Permet de mettre à jour la binavigabilité de tous les éléments (titres dans styles, ...)
        /// </summary>
        public static void UpdateBinavigabilite()
        {
            Titres.ForEach(t =>
            {
                t.LienStyle = LienStyles.Where(ls => ls.IdTitre == t.IdTitre).ToList();
                t.LienStyle.ToList().ForEach(ls =>
                {
                    ls.Style = Styles.FirstOrDefault(style => style.IdStyle == ls.IdStyle);
                });
                t.Commentaires = Commentaires.Where(c => c.IdTitre == t.IdTitre).ToList();
            });

            Artistes.ForEach(artist =>
            {
                artist.Titres = Titres.Where(t => t.Artiste.Nom == artist.Nom).ToList();
            });


            Commentaires.ForEach(comment => comment.Titre = Titres.FirstOrDefault(x => x.IdTitre == comment.IdTitre));
        }
    }
}
