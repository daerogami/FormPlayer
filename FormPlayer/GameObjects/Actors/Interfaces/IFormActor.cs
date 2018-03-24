using System.Windows.Forms;

namespace QuantumGate.GameObjects.Actors.Interfaces
{
    public interface IFormActor: IActor
    {
        string ControlName { get; set; }

        Control GetControl();
    }
}