using System.Windows.Forms;
using QuantumGate.GameObjects.Actors.Interfaces;

namespace QuantumGate.GameObjects.Actors.Abstract
{
    public abstract class FormActor: Actor, IFormActor
    {
        public string ControlName { get; set; }
        protected Control Control { get; set; }

        public virtual Control GetControl()
        {
            return Control;
        }
    }
}