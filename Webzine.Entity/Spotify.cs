using Newtonsoft.Json;
using System.Collections.Generic;

namespace Webzine.Entity
{
    public class SpotifyPlaylist
    {
        [JsonProperty("tracks")]
        public SpotifyTrack Tracks { get; set; }
    }

    public class SpotifyTrack
    {
        [JsonProperty("items")]
        public List<SpotifyTrackItem> Items { get; set; }
    }

    public class SpotifyTrackItem
    {
        [JsonProperty("added_at")]
        public string AddedAt { get; set; }

        [JsonProperty("track")]
        public SpotifyTrackItemTrack Track { get; set; }
    }

    public class SpotifyTrackItemTrack
    {
        [JsonProperty("album")]
        public SpotifyAlbum Album { get; set; }

        [JsonProperty("artists")]
        public List<SpotifyArtist> Artists { get; set; }

        [JsonProperty("duration_ms")]
        public int Duration { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewURL { get; set; }

        [JsonProperty("uri")]
        public string SpotifyUri { get; set; }
    }

    public class SpotifyAlbum
    {
        [JsonProperty("artists")]
        public List<SpotifyArtist> Artists { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("images")]
        public List<SpotifyAlbumImages> Images { get; set; }
    }

    public class SpotifyAlbumImages
    {
        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }
    }

    public class SpotifyArtist
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SpotifyGetToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
