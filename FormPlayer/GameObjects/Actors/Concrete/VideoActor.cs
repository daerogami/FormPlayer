using System;
using AxAXVLC;
using QuantumGate.GameObjects.Actors.Abstract;
using QuantumGate.GameObjects.Contents.Concrete;

namespace QuantumGate.GameObjects.Actors.Concrete
{
    [Serializable]
    public sealed class VideoActor: FormActor
    {
        public Video Content { get; set; }

        private bool _paused;
        private AxVLCPlugin2 VideoControl => Control as AxVLCPlugin2;

        public override void RegisterCues()
        {
            throw new NotImplementedException();
        }

        public override bool IsLoaded => Control != null;
        public override void Load()
        {
            Control = new AxVLCPlugin2();
            if (Control == null)
            {
                throw new FailedToInitializeException("Failed to retrieve control.");
            }
            if (!Control.Created)
            {
                VideoControl.CreateControl();
            }
            VideoControl.playlist.add(Content.Path);
            VideoControl.MediaPlayerStopped += (sender, args) =>
            {
                VideoControl.Visible = false;
                //TODO: Unload??
            };
        }

        public override void Unload()
        {
            VideoControl.Dispose();
        }

        public void Play()
        {
            VideoControl.Visible = true;
            VideoControl.playlist.play();
        }

        public void Pause()
        {
            if (!VideoControl.playlist.isPlaying || _paused) return;
            VideoControl.playlist.pause();
            _paused = true;
        }

        public void Resume()
        {
            if (!_paused) return;
            _paused = false;
            Play();
        }

        public void Stop()
        {
            VideoControl.playlist.stop();
        }
    }
}