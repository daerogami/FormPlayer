using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using QuantumGate.GameObjects.Actors.Concrete;
using QuantumGate.GameObjects.Actors.Interfaces;
using QuantumGate.GameObjects.Beats.Concrete;
using QuantumGate.GameObjects.Common.Interfaces;
using QuantumGate.GameObjects.Cues.Concrete;
using QuantumGate.GameObjects.Cues.Interface;

namespace QuantumGate.GameObjects
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Stage : ILoadable, IHideable
    {
        public string Name { get; }
        public IDictionary<int, Contents.Concrete.Image> Scenes { get; } = new Dictionary<int, Contents.Concrete.Image>();
        public ICollection<IActor> Actors { get; } = new HashSet<IActor>();

        [JsonIgnore]
        protected ICollection<Control> Elements { get; } = new HashSet<Control>();
        [JsonIgnore]
        protected ICollection<Control> HiddenElements { get; } = new HashSet<Control>();


        public Stage(string name)
        {
            Name = name;
        }
        
        public bool IsLoaded { get; set; }
        
        public void Load()
        {
            if (IsLoaded)
            {
                return;
            }
            LoadCurrentScene();
            LoadActors();
            IsLoaded = true;
            StaticControls.GetControlByName("SceneBox").Visible = true; // HIGH: This should probably be part of a scene event called Ready
        }

        private void LoadActors()
        {
            foreach (var actor in Actors)
            {
                actor.Load();
                if (actor is IFormActor formActor)
                {
                    Elements.Add(formActor.GetControl());
                }
            }

            var onLoadActors = Actors.Where(a => a.IsLoaded && a.Cues.Any(c => c is OnLoadCue));
            foreach (var onLoadActor in onLoadActors)
            {
                foreach (var beat in onLoadActor.Cues.OfType<OnLoadCue>().Single().Beats)
                {
                    if (onLoadActor is VideoActor videoActor && beat is TriggerVideoStartBeat)
                    {
                        videoActor.Play();
                    }
                }
            }
        }

        private void LoadCurrentScene()
        {
            if (!(StaticControls.GetControlByName("SceneBox") is PictureBox sceneBox))
            {
                throw new ApplicationException("Static control is missing");
            }
            var sceneIndex = this.GetStateForCurrentStage();
            if (!Scenes.ContainsKey(sceneIndex))
            {
                throw new ApplicationException($"Scene index {sceneIndex} is invalid for scene {Name}");
            }
            var currentScene = Scenes[sceneIndex];
            currentScene.Load();
            var image = currentScene.Data;
            sceneBox.Image = image; // TODO: Index should come from Player data's sceneStateDictionary
        }

        public void Unload()
        {
            if (!IsLoaded)
            {
                return;
            }
            UnloadActors();
            IsLoaded = false;
        }

        public void UnloadActors()
        {
            foreach (var actor in Actors)
            {
                actor.Unload();
            }
        }

        public void Hide()
        {
            foreach (var videoActor in Actors.OfType<VideoActor>())
            {
                videoActor.Pause(); // LOW: Should probably check for any playlists that are playing and only pause those
            }

            foreach (var soundActor in Actors.OfType<SoundActor>())
            {
                soundActor.Stop(); //LOW: If music is being played from here it will not resume
            }

            foreach (var element in Elements)
            {
                if (element.Visible == true)
                {
                    element.Visible = false;
                    HiddenElements.Add(element);
                }
            }
        }

        public void Unhide()
        {
            foreach (var element in HiddenElements)
            {
                element.Visible = true;
            }
            HiddenElements.Clear();

            foreach (var videoActor in Actors.OfType<VideoActor>())
            {
                videoActor.Resume(); // LOW: When we figure out where to save what we paused, need to resume from that item/collection
            }
        }
    }
}