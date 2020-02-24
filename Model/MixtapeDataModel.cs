using System;
using System.Collections.Generic;


namespace HighSpotJson.Model
{
    /// <summary>
    /// Main Mix tape model
    /// </summary>
    public class MixtapeDatamodel
    {
        public List<User> users { get; set; }
        public List<Playlist> playlists { get; set; }
        public List<Song> songs { get; set; }
    }

    public class User
    {
        /// <summary>
        /// for future requirement to add user from change file
        /// </summary>
        /// <value></value>
        public string action { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Playlist
    {
        public string action { get; set; }
        public string id { get; set; }
        public string user_id { get; set; }
        public List<string> song_ids { get; set; }
    }

    public class Song
    {
        /// <summary>
        /// for future requirement to add song from change file
        /// </summary>
        /// <value></value>
        public string action { get; set; }
        public string id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
    }
}
