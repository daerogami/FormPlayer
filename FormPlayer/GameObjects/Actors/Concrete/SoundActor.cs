using System;
using System.Media;
using QuantumGate.GameObjects.Actors.Abstract;
using QuantumGate.GameObjects.Contents.Concrete;

namespace QuantumGate.GameObjects.Actors.Concrete
{
    [Serializable]
    public sealed class SoundActor: Actor
    {
        private SoundPlayer _soundPlayer;
        public Sound Content { get; set; }


        public override void RegisterCues()
        {
            throw new NotImplementedException();
        }

        public override bool IsLoaded => _soundPlayer != null && Content.IsLoaded;

        public override void Load()
        {
            Content.Load();
            _soundPlayer = new SoundPlayer
            {
                Stream = Content.Data
            };
            _soundPlayer.Load(); // LOW: onsider using loadAsync to have parallel loading
        }

        public override void Unload()
        {
            if (!IsLoaded)
            {
                return;
            }
            _soundPlayer.Dispose();
            Content.Unload();
        }

        public void Play()
        {
            if (!IsLoaded)
            {
                throw new InvalidOperationException("Sound has not been initialized");
            }
            _soundPlayer.Play();
        }

        public void Stop()
        {
            if (!IsLoaded)
            {
                throw new InvalidOperationException("Sound has not been initialized");
            }
            _soundPlayer.Stop();
        }
    }
}