using QuantumGate.GameObjects.Beats.Interface;
using QuantumGate.GameObjects.Contents.Concrete;

namespace QuantumGate.GameObjects.Beats.Concrete
{
    public sealed class ChangeStageBeat: IBeat
    {
        public Video TransitionVideo { get; set; }
        public string DestinationStage { get; set; }
    }
}