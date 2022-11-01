using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Webzine.Entity;

namespace Webzine.EntitiesContext
{
    public class SpotifySeeder
    {
        private string _clientId;
        private string _clientSecret;
        private string _tokenBaseUrl;
        private string _accessToken;
        private string _baseUrl;
        private string _playlistRoute;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">ClientID Spotify</param>
        /// <param name="secret">Client Secret Spotify</param>
        public SpotifySeeder(string id, string secret)
        {
            _clientId = id;
            _clientSecret = secret;
            _tokenBaseUrl = "https://accounts.spotify.com/api/token";
            _baseUrl = "https://api.spotify.com/v1";
            _playlistRoute = "/playlists/";
        }

        /// <summary>
        /// Permet de récupérer un nouveau Token via l'API Spotify
        /// </summary>
        /// <returns>Token d'authentification</returns>
        public string GetAccessToken()
        {
            // Encode en base 64 le combo du clientID ainsi que le client Secret
            // Nécéssaire pour requêter l'API de Spotify
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _clientId, _clientSecret)));

            // Création de la requête vers l'API Spotify
            // Voir la documentation sur : https://developer.spotify.com/documentation/web-api/reference/
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_tokenBaseUrl);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Basic " + encoded);

            var request = ("grant_type=client_credentials");
            byte[] req_bytes = Encoding.ASCII.GetBytes(request);
            webRequest.ContentLength = req_bytes.Length;

            Stream strm = webRequest.GetRequestStream();
            strm.Write(req_bytes, 0, req_bytes.Length);
            strm.Close();

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            string json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }

            try
            {
                // Désérialisation de la réponse
                return JsonConvert.DeserializeObject<SpotifyGetToken>(json).AccessToken;
            }

            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Récupère une playlist via l'API Spotify
        /// </summary>
        /// <param name="playlistId">Identifiant de la playlist</param>
        /// <returns></returns>
        public SpotifyPlaylist GetPlaylist(string playlistId)
        {
            // Création de la requête de récupération de la playlist
            // Voir documentation API Spotify : https://developer.spotify.com/documentation/web-api/reference/playlists/get-playlist/
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_baseUrl + _playlistRoute + playlistId);

            webRequest.Method = "GET";
            webRequest.Accept = "application/json";

            // Récupération du token d'accès
            _accessToken = GetAccessToken();

            // Si le token est bien récupéré
            if (_accessToken != "")
            {
                // Préparation de la requête de récupération de la playlist
                webRequest.Headers.Add("Authorization: Bearer " + _accessToken);

                HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
                string json = "";
                using (Stream respStr = resp.GetResponseStream())
                {
                    using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                    {
                        json = rdr.ReadToEnd();
                        rdr.Close();
                    }
                }

                try
                {
                    // Renvoi des données déserialisées
                    return JsonConvert.DeserializeObject<SpotifyPlaylist>(json);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            else
            {
                return new SpotifyPlaylist();
            }
        }

        /// <summary>
        /// Seed database depuis une playlist Spotify
        /// </summary>
        /// <param name="database">DbContext de la base de données à seeder</param>
        /// <param name="playlist">Playlist à ajouter</param>
        /// <returns>Objet contenant les données à seeder</returns>
        public Seeder SeedFromPlaylist(WebzineDbContext database, SpotifyPlaylist playlist)
        {
            // Initialisation des listes
            List<Titre> titres = new List<Titre>();
            List<Artiste> artistes = new List<Artiste>();
            List<Style> styles = new List<Style> { new Style { LienStyle = new List<LienStyle>(), Libelle = "Spotify", IdStyle = 1 } };

            // Récupération du style Spotify
            var spotifyStyle = styles.FirstOrDefault(s => s.Libelle == "Spotify");

            // Pour chacune des musiques récupérées dans la Playlist (depuis l'API Spotify)
            playlist.Tracks.Items.ForEach(track =>
            {
                // Initialisation des objets titres/artistes
                Titre ttr = new Titre();
                Artiste art = new Artiste();

                // Récupération des valeurs depuis l'objet Spotify
                ttr.Libelle = track.Track.Name;
                ttr.Album = track.Track.Album.Name;
                ttr.Chronique = "Chronique de base pour la musique " + ttr.Libelle;
                ttr.Commentaires = new List<Commentaire>();
                ttr.DateCreation = DateTime.Now;
                ttr.DateSortie = DateTime.Parse(track.AddedAt);
                ttr.Duree = track.Track.Duration / 1000;
                // Si l'artiste existe déjà en base, récupération
                if (artistes.Exists(a => a.Nom == track.Track.Artists[0].Name))
                {
                    ttr.Artiste = artistes.FirstOrDefault(a => a.Nom == track.Track.Artists[0].Name);
                    ttr.IdArtiste = ttr.Artiste.IdArtiste;
                }
                // Sinon création de l'artiste
                else
                {
                    art.Nom = track.Track.Artists[0].Name;
                    art.Biographie = "Biographie de " + art.Nom;
                    art.Titres = new List<Titre>();
                    artistes.Add(art);
                    ttr.IdArtiste = art.IdArtiste;
                    ttr.Artiste = art;
                }

                // Si Spotify à bien renvoyé une jaquette pour ce titre
                if (track.Track.Album.Images != null && track.Track.Album.Images.Count > 0)
                {
                    // Attribution
                    ttr.UrlJaquette = track.Track.Album.Images[0].Url;
                }

                // Sinon, initialisé en string vide, les views afficheront une image par défaut (dans wwwroot)
                else
                {
                    ttr.UrlJaquette = "";
                }

                // Si jamais un Uri d'écoute est envoyé
                if (track.Track.SpotifyUri != null)
                {
                    // Et est valide
                    if (track.Track.SpotifyUri.Contains("spotify:track:"))
                    {
                        // Construction de l'URL d'écoute pour le Widget (Dans la page Titre)
                        ttr.UrlEcoute = "https://open.spotify.com/embed/track/" + track.Track.SpotifyUri.Split("spotify:track:")[1];
                    }
                    else
                    {
                        ttr.UrlEcoute = "";
                    }
                }
                else
                {
                    ttr.UrlEcoute = "";
                }
                ttr.NbLectures = 0;
                ttr.NbLikes = 0;

                // Ajout du titre dans la liste
                titres.Add(ttr);
            });

            // Intègre les styles dans les musiques
            artistes.ForEach(artist =>
            {
                artist.Titres = titres.Where(t => t.Artiste.Nom == artist.Nom).ToList();
            });

            // Ajout des listes en bases
            artistes.ForEach(a => database.Artistes.Add(a));
            styles.ForEach(s => database.Styles.Add(s));
            titres.ForEach(t => database.Titres.Add(t));

            return new Seeder() { Artistes = artistes, Titres = titres, Styles = styles };
        }
    }
}
