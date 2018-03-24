using System;
using System.Windows.Forms;
using QuantumGate.GameObjects.Actors.Abstract;
using QuantumGate.GameObjects.Contents.Concrete;

namespace QuantumGate.GameObjects.Actors.Concrete
{
    [Serializable]
    public sealed class ImageActor: FormActor
    {
        public Image Content { get; set; }
        

        public override void RegisterCues()
        {
            throw new NotImplementedException();
        }

        public override bool IsLoaded => Control != null || Content.IsLoaded;
        public override void Load()
        {
            Content.Load();
            Control =  new PictureBox { Name = ControlName, Image = Content.Data};
            if (Control == null)
            {
                throw new FailedToInitializeException("Failed to retrieve control.");
            }
            if (!Control.Created)
            {
                Control.CreateControl();
            }
            RegisterCues();
        }

        public override void Unload()
        {
            Control.Dispose();
            Content.Unload();
        }
    }
}