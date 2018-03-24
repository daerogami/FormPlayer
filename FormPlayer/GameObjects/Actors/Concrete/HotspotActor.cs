using System;
using System.Linq;
using System.Windows.Forms;
using QuantumGate.GameObjects.Actors.Abstract;
using QuantumGate.GameObjects.Beats.Concrete;
using QuantumGate.GameObjects.Contents.Concrete;
using QuantumGate.GameObjects.Cues.Concrete;
using QuantumGate.GameObjects.States.Concrete;

namespace QuantumGate.GameObjects.Actors.Concrete
{
    [Serializable]
    public sealed class HotspotActor: FormActor
    {
        public Image Content { get; set; }

        
        public override bool IsLoaded => Control != null;
        public override void Load()
        {
            Control = StaticControls.GetControlByName(ControlName);
            if (Control == null)
            {
                throw new FailedToInitializeException("Failed to retrieve static control.");
            }
            if (!Control.Created)
            {
                Control.CreateControl();
            }
            if (Content != null && Control is PictureBox pictureBoxControl)
            {
                Content.Load();
                pictureBoxControl.Image = Content.Data;
            }
        }

        public override void Unload()
        {
            Control.Dispose();
            Content.Unload();
        }

        public override void RegisterCues()
        {
            var gameState = StateManager.ActiveState as GameState; // HIGH: This stinks real bad
            if(gameState == null) throw new ApplicationException("GameState could not be retrieved... crap."); //HIGH: Dont leave this here
            Control.Click += (sender, args) =>
            {
                var changeStageBeat = Cues.OfType<OnClickCue>().Single().Beats.OfType<ChangeStageBeat>().Single();
                gameState.PlayTransitionVideo(changeStageBeat.TransitionVideo); // This should live in the static control's class
                // HIGH: Do we need to wait for transition video to finish? Its event subs in GameState _should_ prevent the state from showing before the transition completes
                gameState.ChangeStage(changeStageBeat.DestinationStage);
            };
        }
    }
}