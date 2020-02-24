using System;
using System.Linq;

namespace HighSpotJson
{
    class BatchProcess
    {
        Model.MixtapeDatamodel _inputMixtapeModel = new Model.MixtapeDatamodel();
        Model.MixtapeDatamodel _changeMixtapeModel = new Model.MixtapeDatamodel();
        /// <summary>
        /// constructor with input and change model
        /// </summary>
        /// <param name="inputMixtapeModel"></param>
        /// <param name="changeMixtapeModel"></param>
        public BatchProcess(Model.MixtapeDatamodel inputMixtapeModel, Model.MixtapeDatamodel changeMixtapeModel)
        {
            this._inputMixtapeModel = inputMixtapeModel;
            this._changeMixtapeModel = changeMixtapeModel;
        }

        /// <summary>
        /// process changes and update input to generate output Json 
        /// </summary>
        /// <returns></returns>
        public Model.MixtapeDatamodel ProcessOutputJson()
        {

            try
            {
                foreach (Model.Playlist plist in this._changeMixtapeModel.playlists)
                {
                    switch (plist.action)
                    {
                        case "addplaylist":
                            AddPlayList(plist);
                            break;
                        case "addplaylistsongs":
                            AddPlaylistSongs(plist);
                            break;
                        case "removeplaylist":
                            RemovePlayList(plist);
                            break;
                        case "removeplaylistsongs":
                            RemovePlayListSongs(plist);
                            break;
                        default:
                            //default message or do nothing
                            Console.WriteLine("invalid input");
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogHelper.LogError(ex);
            }
            return this._inputMixtapeModel;
        }

        /// <summary>
        /// add playlist to output file 
        /// </summary>
        /// <param name="plist"></param>
        private void AddPlayList(Model.Playlist plist)
        {
            plist.action = "";
            plist.id = Helper.ModelHelper.GetNewPlayListID(this._inputMixtapeModel);
            this._inputMixtapeModel.playlists.Add(plist);
        }
        /// <summary>
        /// add song in existing playlist
        /// </summary>
        /// <param name="plist"></param>
        private void AddPlaylistSongs(Model.Playlist plist)
        {
            try
            {
                //assuming only occurence of the id - add validation if multiple 
                Model.Playlist pListtoAddSong = this._inputMixtapeModel.playlists.Find(x => x.id == plist.id);
                if (pListtoAddSong != null)
                {
                    //if no validation required we can directly add all the song
                    //but we might add a song in the file only before this openration
                    //so need change before adding all the songs
                    //pListtoAddSong.song_ids.AddRange(plist.song_ids);
                    foreach (string newSongID in plist.song_ids)
                    {
                        if (this._inputMixtapeModel.songs.Where(x => x.id == newSongID).Count() > 0)
                            pListtoAddSong.song_ids.Add(newSongID);
                        else
                            Helper.LogHelper.LogInformation(string.Format("No Song Exists songid:{0}", newSongID), Helper.LogType.Warnings);
                    }
                }
                else
                    Helper.LogHelper.LogInformation(string.Format("No Playlist Exists to add song PlaylistID:{0}", plist.id), Helper.LogType.Warnings);

            }
            catch (Exception ex)
            {
                Helper.LogHelper.LogError(ex);
            }


        }
        /// <summary>
        /// remove existing playlist
        /// </summary>
        /// <param name="plist"></param>
        private void RemovePlayList(Model.Playlist plist)
        {
            //remove if exists - ignore if doesn't exists 
            this._inputMixtapeModel.playlists.RemoveAll(x => x.id == plist.id);
        }
        /// <summary>
        /// remove a song from the existing playlist  
        /// </summary>
        /// <param name="plist"></param>
        private void RemovePlayListSongs(Model.Playlist plist)
        {
            //assuming only occurence of the id - add validation if multiple 
            Model.Playlist pListtoRemoveSong = this._inputMixtapeModel.playlists.Find(x => x.id == plist.id);
            if (pListtoRemoveSong != null)
                foreach (string songid in plist.song_ids)
                    //remove if exists - ignore if doesn't
                    pListtoRemoveSong.song_ids.Remove(songid);
            else
                Helper.LogHelper.LogInformation(string.Format("No Playlist Exists to remove song PlaylistID:{0}", plist.id), Helper.LogType.Warnings);
        }
    }
}
