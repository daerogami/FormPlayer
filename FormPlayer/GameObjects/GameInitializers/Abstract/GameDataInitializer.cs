using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AxAXVLC;
using QuantumGate.GameObjects.GameInitializers.Interface;

namespace QuantumGate.GameObjects.GameInitializers.Abstract
{
    public abstract class GameDataInitializer : IGameDataInitializer
    {
        protected static string CacheFolderPath => $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\QGatePlayer";
        public abstract string CachePath { get; }
        protected string ExternalGameDataPath { get; set; }
        public virtual IStoryData StoryData { get; protected set; }
        protected readonly ProgressBar _progressBar;
        protected bool ForceValidation { get; set; }
        [Obsolete("Fix validation!")]
        protected bool IgnoreValidationExceptions { get; }


        internal GameDataInitializer(ProgressBar progressBar)
        {
            _progressBar = progressBar;
#if DEBUG
            //HIGH: This needs to be fixed, this is currently bypassing an important feature!
            IgnoreValidationExceptions = true;
#endif
        }

        public abstract void VerifyGameData();

        public abstract void BuildGameCache();
        
        internal void ValidateMovies(IEnumerable<string> moviePaths, HashSet<string> badMovies)
        {
            UpdateStatusText(@"Validating Movies");
            var player = new AxVLCPlugin2
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Enabled = true,
                Location = new Point(305, 42),
                Name = "TestVideoPlayer",
                Size = new Size(589, 359),
                TabIndex = 3
            };
            player.CreateControl();
            player.audio.mute = true;
            player.Visible = false;
            foreach (var path in moviePaths)
            {
                try
                {
                    var playlist = player.playlist;
                    if (playlist.isPlaying)
                    {
                        playlist.stop();
                    }
                    playlist.clear();
                    playlist.add(new Uri(ExternalGameDataPath + path).AbsoluteUri);
                    //playlist.play(); //HIGH: Is this required for validation? If so, should immediately stop playback after starting
                }
                catch (ArgumentException e)
                {
                    Debug.WriteLine(e.GetType());
                    badMovies.Add(ExternalGameDataPath + path);
                }
                catch (Exception e) //HIGH: Find out what exception invalid video files throw and filter
                {
                    Debug.WriteLine(e.GetType());
                    badMovies.Add(ExternalGameDataPath + path);
                }
                finally
                {
                    FileValidated();
                }
            }
            player.Dispose();
        }

        internal void ValidateImages(IEnumerable<string> imagePaths, HashSet<string> badImages)
        {
            UpdateStatusText(@"Validating Images");
            foreach (var path in imagePaths.Where(p=>p.EndsWith(".BMP")))
            {
                try
                {
                    var bitmap = new Bitmap(ExternalGameDataPath + path);
                }
                catch (ArgumentException e)
                {
                    Debug.WriteLine(e);
                    badImages.Add(ExternalGameDataPath + path);
                }
                catch (Exception e) //HIGH: Find out what exception invalid bitmap files throw and filter
                {
                    Debug.WriteLine(e);
                    badImages.Add(ExternalGameDataPath + path);
                }
                finally
                {
                    FileValidated();
                }
            }
        }

        internal void ValidateSounds(IEnumerable<string> soundPaths, HashSet<string> badSounds)
        {
            UpdateStatusText(@"Validating Sounds");
            var player = new System.Media.SoundPlayer();
            foreach (var path in soundPaths)
            {
                try
                {
                    player.SoundLocation = ExternalGameDataPath + path;
                    player.Load();
                }
                catch (Exception e) //HIGH: Find out what exception invalid sound files throw and filter
                {
                    Debug.WriteLine(e);
                    badSounds.Add(ExternalGameDataPath + path);
                }
                finally
                {
                    player.Stop();
                    FileValidated();
                }
            }
        }

        private void UpdateStatusText(string status)
        {
            _progressBar.Text = status;
        }

        private void FileValidated()
        {
            _progressBar.PerformStep();
        }
    }
}