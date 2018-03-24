using System;
using System.Diagnostics;
using System.Windows.Forms;
using AxAXVLC;
using QuantumGate.GameObjects.Contents.Concrete;
using QuantumGate.GameObjects.GameInitializers.Interface;
using QuantumGate.GameObjects.States.Abstract;

namespace QuantumGate.GameObjects.States.Concrete
{
    public class GameState : State
    {
        private readonly IStoryData _storyData;
        public override string Id => typeof(GameState).Name;
        public Stage ActiveStage { get; set; }

        
        public GameState(GameWindow gameWindow, IStoryData storyData)
            : base(gameWindow)
        {
            _storyData = storyData;
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializeControls();
            ChangeStage(_storyData.InitialStage);
        }
        
        // HIGH: Game state should not care about these, if a stage needs them, it will grab them and register as needed
        public void InitializeControls()
        {
            var player = StaticControls.GetControlByName("MainVideoPlayer") as AxVLCPlugin2;
            if (player == null)
            {
                throw new NullReferenceException("MainVideoPlayer from static controls is null.");
            }
            Window.AddControl(player); // NOTE: This sets the parent and must be called before creating the control
            player.CreateControl(); // TODO: This is a static object and should be initialized more reliably
            player.audio.mute = true;
            player.Visible = true;
            player.AutoPlay = false;
            player.Toolbar = false;
            player.MediaPlayerPlaying += MainVideoPlayer_MediaPlayerPlaying;
            player.MediaPlayerEndReached += MainVideoPlayer_MediaPlayerEndReached;

            var scenebox = StaticControls.GetControlByName("SceneBox");
            var topNavigation = StaticControls.GetControlByName("TopNavigation");
            var leftNavigation = StaticControls.GetControlByName("LeftNavigation");
            var rightNavigation = StaticControls.GetControlByName("RightNavigation");
            var bottomNavigation = StaticControls.GetControlByName("BottomNavigation");
            topNavigation.Parent = scenebox;
            leftNavigation.Parent = scenebox;
            rightNavigation.Parent = scenebox;
            bottomNavigation.Parent = scenebox;
            Window.AddControl(scenebox);
            Window.AddControl(topNavigation);
            Window.AddControl(leftNavigation);
            Window.AddControl(rightNavigation);
            Window.AddControl(bottomNavigation);
            ResizeControlToParent(player);
            ResizeControlToParent(scenebox);

            Window.Resize += WindowOnResize;

            foreach (var control in StaticControls.StoryNavigationControls)
            {
                control.Visible = false;
            }
        }

        private void WindowOnResize(object sender, EventArgs eventArgs)
        {
            ResizeControlToParent(StaticControls.GetControlByName("MainVideoPlayer"));
            ResizeControlToParent(StaticControls.GetControlByName("SceneBox"));
        }

        // LOW: There has gotta be a better place for this
        private static void ResizeControlToParent(Control control)
        {
            control.Width = control.Parent.Width;
            control.Height = control.Parent.Height;
        }

        public void ChangeStage(string stageName)
        {
            ActiveStage?.Unload();
            var stage = _storyData.GetStageByName(stageName);
            if (stage == null)
            {
                throw new ApplicationException("Stage not found"); //TODO: Need to validate all stage names are keyed in the story data before we finish setting up the gamestate
            }
            Player.UpdateCurrentStage(stage);
            ActiveStage = stage;
            ActiveStage.Load();
        }

        public void PlayTransitionVideo(Video transition)
        {
            var player = StaticControls.GetControlByName("MainVideoPlayer") as AxVLCPlugin2;
            if (player == null)
            {
                throw new NullReferenceException("MainVideoPlayer from static controls is null.");
            }
            player.playlist.add(transition.Path);
            player.playlist.play();
        }

        private static void MainVideoPlayer_MediaPlayerPlaying(object sender, EventArgs e)
        {
            StaticControls.GetControlByName("MainVideoPlayer").Visible = true;
            StaticControls.GetControlByName("SceneBox").Visible = false;
        }

        private static void MainVideoPlayer_MediaPlayerEndReached(object sender, EventArgs e)
        {
            StaticControls.GetControlByName("MainVideoPlayer").Visible = false;
            StaticControls.GetControlByName("SceneBox").Visible = true;
        }

        public override void Pause()
        {
            base.Pause();
            ActiveStage.Hide();
        }

        public override void Resume()
        {
            base.Resume();
            ActiveStage.Unhide();
        }
    }
}